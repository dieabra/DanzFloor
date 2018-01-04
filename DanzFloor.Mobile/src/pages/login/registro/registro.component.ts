import { Component, OnInit } from '@angular/core';
import { Nav, NavParams } from 'ionic-angular';
import { SpinnerDialog } from '@ionic-native/spinner-dialog';

import { AccountService } from "../../../services/account.service";
import { SharedService } from '../../../services/shared.service';
import { HomePage } from "../../home/home.page";
import { GoogleAnalytics } from '@ionic-native/google-analytics';

import { AlertController } from 'ionic-angular';

@Component({
    selector: 'registro',
    templateUrl: './registro.component.html',
    providers:[AccountService, SharedService, SpinnerDialog]
})
export class RegistroComponent implements OnInit {
    registroModelo = {
            Email:"",
            Password:"",
            ConfirmPassword:"",
            Name:"",
            Lastname:"",
            RegisterSocial:null
        }
    formulario = {
            Email:false,
            Password:false,
            ConfirmPassword:false,
            Name:false,
            Lastname:false,
            Formulario: false
        }    
    constructor(
        private accountService:AccountService,
        private nav: Nav,
        private sharedService:SharedService,
        private spinnerDialog: SpinnerDialog,
        public navParams: NavParams,
        private ga: GoogleAnalytics,
        private alertCtrl: AlertController
        ) { }

        esRegistroSocial:boolean = false;

    ngOnInit() {
        this.ga.trackView('View: Registro');
        if(this.navParams.get('social') != null && this.navParams.get('social') != undefined){   
            let social = this.navParams.get('social');
            this.registroModelo.Email = social.Email;
            this.registroModelo.Lastname = social.Apellido;
            this.registroModelo.Name = social.Nombre;
            this.registroModelo.RegisterSocial = social;
            this.registroModelo.Password = "qweQWE123!#";
            this.registroModelo.ConfirmPassword = "qweQWE123!#";
            this.esRegistroSocial = true;
        }
        
        this.validarFormulario()
     }

	Registrar(){
        this.ga.trackEvent('Registrar', 'Registrar');
        //this.spinnerDialog.show('Registración', 'Validando los datos ingresados...', true);
			
        this.accountService.Registrar(this.registroModelo)
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
                    this.sharedService.insertUsuario(respuesta.Usuario.Usuario, "");
                    console.log(respuesta);
                    this.nav.setRoot(HomePage);
                    //this.spinnerDialog.hide();
                }
            },
            error => { console.log(error) });
	}

    validarFormulario(){
        // this.formulario.ConfirmPassword = this.registroModelo.Password == this.registroModelo.ConfirmPassword;
        // this.formulario.Email = (this.registroModelo.Email.indexOf('@') != -1) && (this.registroModelo.Email.indexOf('.') != -1);
        // this.formulario.Lastname = this.registroModelo.Lastname.length > 0;
        // this.formulario.Name = this.registroModelo.Name.length > 0;
        // this.formulario.Password = this.registroModelo.Password.length > 6;

        // this.formulario.Formulario = this.formulario.ConfirmPassword &&
        //                                 this.formulario.Email &&
        //                                 this.formulario.Lastname &&
        //                                 this.formulario.Name &&
        //                                 this.formulario.Password;
        this.formulario.Formulario = true;
    }
}