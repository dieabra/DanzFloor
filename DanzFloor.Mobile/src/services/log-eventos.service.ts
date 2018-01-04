import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { EntornoConfig } from "../constants/entorno.config";
import { Observable } from 'rxjs/Observable';

import { SharedService } from "../services/shared.service";
import { Injector } from '@angular/core';
import { MyApp } from '../app/app.component'

import { GoogleAnalytics } from '@ionic-native/google-analytics';

@Injectable()
export class LogEventoService {
    private url = '/logevento/';
    private headers: Headers;
    private entornoConfig: EntornoConfig = new EntornoConfig();
    private myApp:MyApp;

    constructor(private http: Http,  private sharedService: SharedService,private inj:Injector, private ga: GoogleAnalytics) {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/x-www-form-urlencoded');
        this.myApp = inj.get(MyApp);
    }

     LogEvento = (nombre:string, accionid:number, entidadid: string, tipoentidad:number): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        var model = {
            Accion: accionid,
            EntidadId: entidadid,
            TipoEntidad: tipoentidad,
            Nombre: nombre,
            EsMobile: true
        }

        let accion: string = 'GuardarEvento';
        console.log(accion);

        let body = new FormData();
        body.append('usuarioId', this.sharedService.usuario.Id);
        body.append('tokenId', this.sharedService.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));

        return this.http.post(this.sharedService.GetCore() + this.url + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
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
   


   

