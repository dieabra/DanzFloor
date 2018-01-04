import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { SocialSharing } from '@ionic-native/social-sharing';
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { ArtistaPerfilComponent } from "../artistaPerfil/artistaPerfil.component";
import { EventoComponent } from "../evento/evento.component";


@Component({
    selector: 'index',
    templateUrl: './index.component.html'
})
export class IndexComponent{
	private nav: NavController;

	constructor(nav: NavController, 
		private ga: GoogleAnalytics) {		
		this.nav = nav;
	}

	ngOnInit(): void {
		
	}

	verPerfil(): void{
		this.nav.push(ArtistaPerfilComponent);
	}

	verEvento(): void{
		this.nav.push(EventoComponent);
	}


}