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
export class EpisodioService {
    private url = '/episodio/';
    private headers: Headers;
    private entornoConfig: EntornoConfig = new EntornoConfig();
    private myApp:MyApp;

    constructor(private http: Http,  private sharedService: SharedService,private inj:Injector, private ga: GoogleAnalytics) {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/x-www-form-urlencoded');
        this.myApp = inj.get(MyApp);
    }

     GetEpisodio = (episodioId): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        var model = {
            VideoId: episodioId,
            OneSignal: this.sharedService.usuario.OneSignal,
            EsMobile: true
        }

        let accion: string = 'GetEpisodio';
        console.log(accion);

        let body = new FormData();
        body.append('usuarioId', this.sharedService.usuario.Id);
        body.append('tokenId', this.sharedService.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));

        return this.http.post(this.sharedService.GetCore() + "/episodio/" + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    Novedades = (pageIndex, pageSize): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        let accion: string = 'get';
        console.log(accion);

        var model = {
            PageIndex:pageIndex,
            PageSize:pageSize,
            EsMobile: true,
            OneSignal: this.sharedService.usuario.OneSignal
        }

        let body = new FormData();
        body.append('usuarioId', this.sharedService.usuario.Id);
        body.append('tokenId', this.sharedService.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));
        return this.http.post(this.sharedService.GetCore() + this.url + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    EpisodisBookmark = (pageIndex, pageSize): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        let accion: string = 'Bookmark';
        console.log(accion);

        var model = {
            PageIndex:pageIndex,
            PageSize:pageSize,
            OneSignal: this.sharedService.usuario.OneSignal
        }

        let body = new FormData();
        body.append('usuarioId', this.sharedService.usuario.Id);
        body.append('tokenId', this.sharedService.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));

        return this.http.post(this.sharedService.GetCore() + this.url + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    Like = (episodioId, esLike): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        var model = {
            episodioId:episodioId,
            esLike:esLike,
            userId:this.sharedService.usuario.Id,
            esMobile: true,
            OneSignal: this.sharedService.usuario.OneSignal
        }

        let accion: string = 'LikeEpisodio';
        console.log(accion);

        let body = new FormData();
        body.append('usuarioId', this.sharedService.usuario.Id);
        body.append('tokenId', this.sharedService.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));

        return this.http.post(this.sharedService.GetCore() + "/like/" + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    Bookmark = (episodioId, esAgregarBookmark): Observable<any> => {
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        var model = {
            episodioId:episodioId,
            esAgregarBookmark:esAgregarBookmark,
            userId:this.sharedService.usuario.Id,
             esMobile: true,
             OneSignal: this.sharedService.usuario.OneSignal
        }

        let accion: string = 'BookmarkEpisodio';
        console.log(accion);

        let body = new FormData();
        body.append('usuarioId', this.sharedService.usuario.Id);
        body.append('tokenId', this.sharedService.usuario.Token);
        body.append('version', this.entornoConfig.CoreVersion);
        body.append('contenido', JSON.stringify(model));

        return this.http.post(this.sharedService.GetCore() + "/Bookmark/" + accion, body, this.headers)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    PorPrograma = (programaId, pageIndex, pageSize, videoId): Observable<any> => {
        if(videoId == undefined || videoId == null || videoId == "")
            videoId = "00000000-0000-0000-0000-000000000000";

        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            return Observable.throw('Sin conexion');  
        }
        var model = {
            ProgramaId:programaId,
            PageIndex:pageIndex,
            PageSize:pageSize,
            VideoId: videoId,
            EsMobile: true,
            OneSignal: this.sharedService.usuario.OneSignal
        }

        let accion: string = 'PorPrograma';
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
   


   

