import { Component, OnInit } from '@angular/core';
import { Platform, Nav} from 'ionic-angular';
import { InAppBrowser } from "ionic-native";

import { EntornoConfig } from "../../constants/entorno.config";

import { SharedService } from '../../services/shared.service';

import { LoginComponent } from '../login/login.component';
import { AppVersion } from '@ionic-native/app-version';

import { GoogleAnalytics } from '@ionic-native/google-analytics';


@Component({
    selector: 'cuenta',
    templateUrl: './cuenta.component.html',
    providers: [SharedService, EntornoConfig, AppVersion]
})
export class CuentaComponent implements OnInit {
    usuario: any;
    programas: any[] = [];
    version:string;
    
    constructor(
        private platform: Platform,
        private sharedService: SharedService,
        private nav: Nav,
		private entornoConfig: EntornoConfig,
        private appVersion: AppVersion,
        private ga: GoogleAnalytics
        ){}

    ngOnInit(){
       
    }

	doRefresh(refresher) {
		
	}

    launch(url) {
        this.platform.ready().then(() => {
            let browser = new InAppBrowser(url, "_blank");
        });
    }

    salir(){
        this.nav.setRoot(LoginComponent);
    }

	ObtenerImagenProgramaPrincipal(imagen): any {
		return '';
	}
}