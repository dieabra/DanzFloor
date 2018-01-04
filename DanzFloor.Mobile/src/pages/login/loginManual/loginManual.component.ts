import { Component, OnInit } from '@angular/core';
import { Nav, NavController } from 'ionic-angular';

import { AccountService } from "../../../services/account.service";
import { SharedService } from '../../../services/shared.service';

import { HomePage } from "../../home/home.page";
import { GoogleAnalytics } from '@ionic-native/google-analytics';

import { AlertController } from 'ionic-angular';



@Component({
    selector: 'loginManual',
    templateUrl: './loginManual.component.html',
    providers: [
        AccountService,
        SharedService
    ]
})
export class LoginManualComponent implements OnInit {
	private loginModel: any = {
		Email: "",// "administrador@registrofirmas.web",
		Password:"",// "ADV770o803"        
	}
    private errorMsg: string;

    constructor(
        private accountService: AccountService,
        private sharedService: SharedService,
        private nav: Nav,
        private navCtrl: NavController,
        private ga: GoogleAnalytics,
        private alertCtrl: AlertController
    ) { 
    }

    ngOnInit() {
        this.ga.trackView('View: Login No Social');
    }

    login() {
        this.ga.trackEvent('Login', 'Login No Social');
        this.accountService.Login(this.loginModel)
            .subscribe(respuesta => {
                if (respuesta.Resultado == "Error"){
                    let alert = this.alertCtrl.create({
                        title: 'Error!',
                        subTitle: respuesta.Mensaje,
                        buttons: ['Ok']
                    });
                    alert.present();
                }
                else{
                this.sharedService.insertUsuario(respuesta.Usuario.Usuario, respuesta.Usuario.FotoPerfil);
                console.log(respuesta);
                this.nav.setRoot(HomePage);
                }
            },
            error => { console.log(error) });
    }
}