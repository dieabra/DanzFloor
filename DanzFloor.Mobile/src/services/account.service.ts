import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { EntornoConfig } from "../constants/entorno.config";
import { Observable } from 'rxjs/Observable';

import { SharedService} from "../services/shared.service";
import { Injector } from '@angular/core';
import { MyApp } from '../app/app.component'

import { GoogleAnalytics } from '@ionic-native/google-analytics';

@Injectable()
export class AccountService {
    private url = '/account/';
    private headers: Headers;
    private entornoConfig: EntornoConfig = new EntornoConfig();
    private sharedService: SharedService;
    private myApp:MyApp;

    constructor(private http: Http,  private _sharedService: SharedService,private inj:Injector, private ga: GoogleAnalytics) {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/x-www-form-urlencoded');
        this.sharedService = _sharedService;
        this.myApp = inj.get(MyApp);
    }

    Login = (model: any): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }

        let accion: string = 'LoginMobile';
        console.log(accion);

        model.Version = this.entornoConfig.CoreVersion;
         if (model.Email.indexOf('#ambientetest') != -1){
            this.sharedService.SetCore(this.entornoConfig.TestEnviroment)
            model.Email = model.Email.replace('#ambientetest','')            
        }
        else{this.sharedService.SetCore(this.entornoConfig.Core)}
        let body = new FormData();
        model.EsMobile = true;
        body.append('contenido', JSON.stringify(model));
       
       
        return this.http.post(this.sharedService.GetCore() + this.url + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(err =>this.handleError(err));
    }

    LoginS = (tipo:number, codigo:string):Observable<any> => {      
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        let accion: string = 'LoginSocial';
        console.log(accion);
        
        let body = new FormData();
        //model.Version = this.entornoConfig.CoreVersion;
        let model = {tipo:tipo,codigo:codigo,Version:this.entornoConfig.CoreVersion, EsMobile: true};
        body.append('contenido', JSON.stringify(model));
        return this.http.post(this.sharedService.GetCore() + '/Cuenta/'+accion, body , this.headers)
            .map((response: Response) => <any>response.json())
            .catch(err =>this.handleError(err));
    }

    Dummy = ():Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        let accion: string = 'Dummy';
        console.log(accion);

        let bodyPrograma = new FormData();
        bodyPrograma.append('usuarioId', this.sharedService.usuario.Id);
        bodyPrograma.append('tokenId', this.sharedService.usuario.Token);
        bodyPrograma.append('version', this.entornoConfig.CoreVersion);

        return this.http.post(this.sharedService.GetCore() + '/Cuenta/' + accion, bodyPrograma, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(err =>this.handleError(err));
    }

    Registrar = (model): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        

        let accion: string = 'RegisterMobile';
        console.log(accion);

        let body = new FormData();
        body.append('contenido', JSON.stringify(model));

        return this.http.post(this.sharedService.GetCore() + this.url + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(err =>
            
                this.handleError(err)
            
            );
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
        try{
            (window as any).FirebaseCrashReport.log(errMsg);
        }
        catch(ex){

        }
        return Observable.throw(errMsg);
    }

}
   


   

