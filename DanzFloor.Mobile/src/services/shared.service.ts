import { Injectable } from '@angular/core';
import { URLSearchParams } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { EntornoConfig } from "../constants/entorno.config";
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers } from '@angular/http';
import { MyApp } from '../app/app.component'

import { GoogleAnalytics } from '@ionic-native/google-analytics';

import { MediaPlayerService } from '../services/media-player.service'


@Injectable()
export class SharedService {
    private headers: Headers;
    private myApp:MyApp;
    constructor(private ga: GoogleAnalytics, private http: Http,) {
        let user = localStorage.getItem('usuario');
        if (user != null && user != 'undefined')
            this.usuario = JSON.parse(localStorage.getItem('usuario'));
        
        this.urlReturnCrearUser = localStorage.getItem('urlReturnCrearUser');
        this.urlReturnDetalleEvento = localStorage.getItem('urlReturnDetalleEvento');
    }

    get networkConnected():boolean {
        return localStorage.getItem('networkConnected') == 'true';
    }
    set networkConnected(theBar:boolean) {
        localStorage.setItem('networkConnected', JSON.stringify(theBar));
    }
    
    usuario: any;
    entornoConfig: EntornoConfig = new EntornoConfig();
    urlReturnCrearUser: string;
    urlReturnDetalleEvento: string;
    Core: string= this.entornoConfig.Core; 
    actividad: any;

    SetDefaultCore() :any {
          localStorage.setItem('Core',this.entornoConfig.Core);
    }
    SetCore(core:string):any{
         localStorage.setItem('Core',core);
    }

    GetCore(){
        return localStorage.getItem('Core');
    }
    
    setOneSignal(oneSignal:any){
        localStorage.setItem('oneSignal',JSON.stringify(oneSignal));
    }

    getOneSignalId():any{
        let res = JSON.parse(localStorage.getItem('oneSignal'));
        try{
            localStorage.getItem('usuario');
            this.usuario["OneSignal"] = res.userId;
            localStorage.setItem('usuario', JSON.stringify(this.usuario));
        }
        catch(ex){}
        return res;
    }

    getOneSignal():any{
        let res = JSON.parse(localStorage.getItem('oneSignal'));
        localStorage.removeItem('oneSignal');
        return res;
    }

    insertUsuario(usuario: any, fotoPerfil: any) {        
        this.usuario = usuario;  
        
          if (this.usuario != null && this.usuario != "" && this.usuario.Email.indexOf('.ambientetest') != -1)
            this.Core = this.entornoConfig.TestEnviroment;
        else
            this.Core = this.entornoConfig.Core;        
        if (usuario == '')
            this.usuario = null;
        if (usuario != null && usuario != '') {  //por si es logout  
            this.usuario["fotoPerfil"] = fotoPerfil;
            this.usuario["OneSignal"] = "test";
        }
        localStorage.setItem('usuario', JSON.stringify(this.usuario));
    }

    UserIsInRol(roles: string[]): boolean {
        for (let i = 0; i < roles.length; i++) {
            if (this.usuario.Roles.findIndex(x => x == roles[i]) > -1) {
                return true;
            }
        }
        return false;
    }

    GetRolesUsuario(): string[] {
        return this.usuario.Roles;
    }

    EncabezadoAuthExiste(): boolean {
        let encabezado = localStorage.getItem('encabezadoAutenticacion');
        if (encabezado != null)
            return true
        return false;
    }

    objToSearchParams(obj: any): URLSearchParams {
        let params: URLSearchParams = new URLSearchParams();
        for (var key in obj) {
            if (obj.hasOwnProperty(key))
                params.set(key, obj[key]);
        }
        return params;
    }

    ObtenerUltimoReproductorActivo (): string{
        return (localStorage.getItem('activeJWPlayer'));
    }

     ObtenerNuevoIdParaJWPlayer () :string{  
        let newPlayer = "MyMediaDiv" + (Math.floor(Math.random() * (10000 - 1)) + 1).toString();
        localStorage.setItem('activeJWPlayer',newPlayer);
        return newPlayer;
    }

    EstablecerUltimoReproductorActivo(player:string){
         localStorage.setItem('activeJWPlayer',player);
         return null;
    }

      LogEvento = (nombre:string, accionid:number, entidadid: string, tipoentidad:number): Observable<any> => {
        if(!this.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        
        var model = {
            Accion: accionid,
            EntidadId: entidadid,
            TipoEntidad: tipoentidad,
            Nombre: nombre,
            EsMobile: true,
            OneSignal: this.usuario.OneSignal
        }

        let accion: string = 'GuardarEvento';

        let body = new FormData();
        body.append('usuarioId', this.usuario.Id);
        body.append('tokenId', this.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));

        if (accionid == 9)
            this.ga.trackEvent('Reproducir', 'Reproducir ' + nombre );
        else if (accionid ==11)
            this.ga.trackEvent('Reproducir', 'Fin Reproduccion ' + nombre );

        return this.http.post(this.GetCore() + '/LogEvento/' + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(err =>this.handleError(err));

    }


     private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;

        if (error instanceof Response) {
            const body = error.json() || '';
            if (body.Result || body.Errors) {
                errMsg = JSON.stringify(body);
            }
            else {
                const err = body.error || JSON.stringify(body);
                errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
            }
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        this.ga.trackException(errMsg,false);
        //(window as any).FirebaseCrashReport.log(errMsg);
        return Observable.throw(errMsg);
    }
}