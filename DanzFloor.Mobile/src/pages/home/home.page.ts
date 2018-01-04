import { Component } from '@angular/core';
import { SharedService } from "../../services/shared.service";
import { GoogleAnalytics } from '@ionic-native/google-analytics';

import { IndexComponent } from "../index/index.component";
import { AgendaComponent } from "../agenda/agenda.component";
import { BusquedaComponent } from "../busqueda/busqueda.component";
import { CuentaComponent } from "../cuenta/cuenta.component";
import { ArtistasComponent } from "../artistas/artistas.component";

@Component({
	templateUrl: 'home.html',
	providers: [SharedService]
})
export class HomePage {
	tabIndex:any;
	tabAgenda:any;
	tabBusqueda:any;
	tabCuenta:any;
	tabArtistas:any;

	badgeValue1 = 0;	
	badgeValue2 = 0;
	badgeValue3 = 0;
	badgeValue4 = 0;
	badgeValue5 = 0;

	constructor(private ga: GoogleAnalytics,
		private sharedService: SharedService) {		
		this.tabIndex = IndexComponent;
		this.tabAgenda = AgendaComponent;
		this.tabBusqueda = BusquedaComponent;
		this.tabCuenta = CuentaComponent;
		this.tabArtistas = ArtistasComponent;

	}


	private initTiles(): void {
		
	}

	analitycsIndex() {
		this.ga.trackView('View: Index');
    }

    analitycsAgenda() {
		this.ga.trackView('View: Agenda');
    }

    analitycsBusqueda() {
		this.ga.trackView('View: Busqueda');
    }

    analitycsCuenta() {
		this.ga.trackView('View: Cuenta');
    }

    analitycsArtistas() {
		this.ga.trackView('View: Artistas');
    }

}
