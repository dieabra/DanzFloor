import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { SocialSharing } from '@ionic-native/social-sharing';
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { IndexComponent } from "../index/index.component";

@Component({
    selector: 'artistas',
    templateUrl: './artistas.component.html'
})
export class ArtistasComponent{
	private nav: NavController;

	constructor(nav: NavController, 
		private ga: GoogleAnalytics) {		
		this.nav = nav;
	}

	ngOnInit(): void {
		
	}

	public itemTapped(item) {
		this.nav.push(IndexComponent);
	}
}