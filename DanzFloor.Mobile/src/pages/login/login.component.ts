import { Component, OnInit } from '@angular/core';
import { Nav, NavController } from 'ionic-angular';

import { AccountService } from "../../services/account.service";
import { SharedService } from '../../services/shared.service';

import { HomePage } from "../home/home.page";
import { LoginManualComponent } from "./loginManual/loginManual.component";
import { RegistroComponent } from "./registro/registro.component";
import { GooglePlus } from '@ionic-native/google-plus'
import { Facebook, FacebookLoginResponse } from '@ionic-native/facebook';
import { Platform } from 'ionic-angular';
import { InAppBrowser } from '@ionic-native/in-app-browser';
import { Injector } from '@angular/core';
import { MyApp } from '../../app/app.component'
import { Observable } from 'rxjs/Observable';

import { SplashScreen } from '@ionic-native/splash-screen';
import { isCordovaAvailable } from '../../services/is-cordova-available';
import { GoogleAnalytics } from '@ionic-native/google-analytics';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    providers: [
        AccountService,
        SharedService,
        GooglePlus,
        InAppBrowser,
        Facebook,
        SplashScreen,
        LoginManualComponent
    ]
})
export class LoginComponent implements OnInit {
	private loginModel: any = {
		Email: "administrador@registrofirmas.web",
		Password: "ADV770o803"
	}
    myApp:MyApp;
    mostrarLogIn: any = false;
    signalId: string = "signalIdstr";

    constructor(
        private accountService: AccountService,
        private sharedService: SharedService,
        private nav: Nav,
        private navCtrl: NavController,
        private googlePlus: GooglePlus,
        private fb: Facebook,
        private platform: Platform,
        private iab: InAppBrowser,
        private inj:Injector, 
        private ga: GoogleAnalytics
    ) { 
        this.myApp = inj.get(MyApp);
    }

    ngOnInit() {
        this.signalId = JSON.stringify(this.sharedService.getOneSignalId());
         this.ga.trackView('View: Login');
         this.sharedService.SetDefaultCore();
        if(this.sharedService.usuario != null)
        {
            this.accountService.Dummy().subscribe(res=> {
                if(res.Resultado == "Ok")
                    this.nav.setRoot(HomePage);
                else
                    this.mostrarLogIn = true;
            },
            err => {
                console.log(err);
                this.mostrarLogIn = true;
            });
        } else {
            this.mostrarLogIn = true;
        }
    

            //this.nav.setRoot(HomePage);

        // if(!this.platform.is('core')){
        //     this.googlePlus.trySilentLogin({
        //         //'scopes': '... ', // optional, space-separated list of scopes, If not included or empty, defaults to `profile` and `email`.
        //         'webClientId': '798083776747-irfipedjps0m8ghr3qdt2u9fo8npc74o.apps.googleusercontent.com', // optional clientId of your Web application from Credentials settings of your project - On Android, this MUST be included to get an idToken. On iOS, it is not required.
        //         'offline': true, // optional, but requires the webClientId - if set to true the plugin will also return a serverAuthCode, which can be used to grant offline access to a non-Google server
        //         }).then(p=>{
        //         alert(':D Email: '+p.email );
        //     })
        // }

    }

    login() {
        this.navCtrl.push(LoginManualComponent);
    }
    
    loginG() {
        this.ga.trackEvent('Login', 'Google Login');
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            throw Observable.throw('Sin conexion');  
        }
        this.googlePlus.login({
            //'scopes': '... ', // optional, space-separated list of scopes, If not included or empty, defaults to `profile` and `email`.
            'webClientId': '325904749795-tebrctnv70i7sjeab6jsf050mad75kcu.apps.googleusercontent.com', // optional clientId of your Web application from Credentials settings of your project - On Android, this MUST be included to get an idToken. On iOS, it is not required.
            'offline': true, // optional, but requires the webClientId - if set to true the plugin will also return a serverAuthCode, which can be used to grant offline access to a non-Google server
        }).then(p => {
            //alert(':D Email: '+p.email );
            let a = p.idToken;
            this.accountService.LoginS(0, p.idToken).subscribe(res => this.respuestaLoginSocial(res, p.idToken), err => alert(err))
        }).catch(p => alert('No es posible conectar con Google'));
    }

    loginF() {
        this.ga.trackEvent('Login', 'Login Facebook');
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            throw Observable.throw('Sin conexion');  
        }
        
        this.fb.login(['public_profile', 'user_friends', 'email'])
            .then((res: FacebookLoginResponse) => {
                console.log('Logged into Facebook!', res);
                this.accountService.LoginS(2, res.authResponse.accessToken)
                    .subscribe(resp => {
                        this.respuestaLoginSocial(resp, res.authResponse.accessToken);
                    },
                    err => alert(err)
                    );
            })
            .catch(e => { alert('No es posible conectar con Facebook'); console.log('Error logging into Facebook', e) });
    }

    loginI() {
        this.ga.trackEvent('Login', 'Login Instagram');
        if(!this.sharedService.networkConnected){
            this.myApp.Toast("Sin acceso a internet :(");
            throw Observable.throw('Sin conexion');  
        }
        let url = 'https://www.instagram.com/oauth/authorize/?client_id=c6c24aef3ec044b6b8c16dbf4bafc573&redirect_uri=https://api.ionic.io/auth/integrations/instagram&response_type=code';
        const browser = this.iab.create(url, "_blank", { location: 'no' });

        browser.on("loadstart").subscribe(ev => {            
            console.log(ev)
            if (ev.url.indexOf("code=") > -1) {
                let indexOf = ev.url.indexOf("code=");
                let codigo = ev.url.substr(indexOf + 5, 32);
                //Mandar el codigo al back para que resuelva el access Token
                console.log(codigo);
                browser.close();
                this.accountService.LoginS(1, codigo).subscribe(res => this.respuestaLoginSocial(res, codigo), err => alert(err))
            }

        })
    }

    respuestaLoginSocial(resp: any, codigo: any) {
        console.log(resp);   
       
        if (resp.Token == null || resp.Token == undefined || resp.Token == ""){
            //resp.Codigo = codigo;
            //this.navCtrl.push(RegistroComponent, { 'social': resp });
            
            alert(JSON.stringify(resp.Mensaje));
        }
        else {           
           
            this.sharedService.insertUsuario(resp.Usuario.Usuario,resp.Usuario.FotoPerfil);
            this.ga.setUserId(resp.Usuario.Usuario.Id);
            console.log(resp);
            this.nav.setRoot(HomePage);
        }
    }

    registro() {
        this.navCtrl.push(RegistroComponent);
    }

    loginDiego() {
        this.nav.setRoot(HomePage);
    }
}