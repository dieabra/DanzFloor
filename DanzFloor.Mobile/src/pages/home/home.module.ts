import { IonicModule } from 'ionic-angular';
import { NgModule } from '@angular/core';
import { HomePage } from './home.page';

import { IndexComponent } from "../index/index.component";
import { AgendaComponent } from "../agenda/agenda.component";
import { BusquedaComponent } from "../busqueda/busqueda.component";
import { CuentaComponent } from "../cuenta/cuenta.component";
import { ArtistasComponent } from "../artistas/artistas.component";
import { ArtistaPerfilComponent } from "../artistaPerfil/artistaPerfil.component";
import { EventoComponent } from "../evento/evento.component";
import { VenueComponent } from "../venue/venue.component";


import { PipesModule } from '../../pipes/pipes.module';
import { SocialSharing } from '@ionic-native/social-sharing';

@NgModule({
	imports: [IonicModule,PipesModule],
	declarations: [
		HomePage, 
		IndexComponent,
		AgendaComponent,
		BusquedaComponent,
		CuentaComponent,
		ArtistasComponent,
		ArtistaPerfilComponent,
		EventoComponent,
		VenueComponent		
		],
	entryComponents: [
		HomePage, 
		IndexComponent,
		AgendaComponent,
		BusquedaComponent,
		CuentaComponent,
		ArtistasComponent,
		ArtistaPerfilComponent,
		EventoComponent,
		VenueComponent
		],
	providers: [
		SocialSharing		
	]
})

export class HomeModule {

}