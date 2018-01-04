import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { EntornoConfig } from "../../../constants/entorno.config";
import { VimeoService } from '../../../services/vimeo.service';
import { ProgramaService } from '../../../services/programa.service';
import { SharedService } from '../../../services/shared.service';
import { SocialSharing } from '@ionic-native/social-sharing';
import { VideoItem } from '../videoItem/videoItem.component'
import { CanalItem } from '../canalItem/canalItem.component'
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { OneSignal } from '@ionic-native/onesignal';

@Component({
    selector: 'programas',
    templateUrl: './programas.component.html',
	providers: [VimeoService, ProgramaService, SharedService, EntornoConfig]
})
export class ProgramasComponent{
    private service: VimeoService;
	private nav: NavController;
	private sharedService: SharedService;

	public programas: any[] = [];
	
	PageIndex = 1;
	PageSize = 3;
	Pages = null;

	constructor(service: VimeoService, nav: NavController, private programaService: ProgramaService,
		private entornoConfig: EntornoConfig,
		private ga: GoogleAnalytics,		
		private socialSharing: SocialSharing,
		private _sharedService: SharedService,
		private oneSignal:OneSignal) {
		this.service = service;
		
		this.nav = nav;
	}

	ngOnInit(): void {
		 this.ga.trackView('View: Programas');
		this.programaService.TraerTodos(this.PageIndex, this.PageSize)
            .subscribe(respuesta => {
                this.programas = respuesta.Result;
            },
            error => { console.log(error) });
	}

	ObtenerImagen(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-160|width-111|mode-crop|quality-100|';
	}

	ObtenerImagenPrograma(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-40|width-40|mode-crop|quality-100|';
	}

	public itemTappedEpisodio(item) {
		this.nav.push(VideoItem, {
			item: item
		});
	}

	public itemTapped(item) {
		this.nav.push(CanalItem, {
			item: item
		});
	}

	doRefresh(refresher) {
		this.ga.trackEvent('Bookmark', 'Refresh');
		this.PageIndex = 1;
		this.programaService.TraerTodos(this.PageIndex, this.PageSize)
            .subscribe(respuesta => {
                this.programas = respuesta.Result;
				refresher.complete();
            },
            error => { console.log(error) });
	}

	seguirPrograma(programa){
		this.ga.trackEvent('Programa', 'Favorito');
		programa.EsFavorito = !programa.EsFavorito;
		
		if(programa.EsFavorito)
			this.oneSignal.sendTag(programa.Id,'favorito');
		else
			this.oneSignal.deleteTag(programa.Id);
			
		this.programaService.seguirPrograma(programa.Id, programa.EsFavorito)
			.subscribe(respuesta => {
				
			},
			error => { console.log(error) });
	}

	share(episodio: any) {
		this.socialSharing.shareWithOptions({ url: this._sharedService.Core + '/FrontEnd/Reproducir/' + episodio.Id })
	}

	doInfinite(): Promise<any> {

		return new Promise((resolve) => {

			this.PageIndex = this.PageIndex + 1;
			
			if(this.Pages != null && this.PageIndex > this.Pages)
				{
					resolve();
					return;
				}
            this.programaService.TraerTodos(this.PageIndex, this.PageSize)
            .subscribe(respuesta => {
				this.ga.trackEvent('Programa', 'ObtenerMasProgramas');
				for (var i = 0; i < respuesta.Result.length; i++) {
					this.programas.push(respuesta.Result[i]);
				}
				this.Pages = respuesta.Pages;
				console.log('Finaliza la carga ' + this.PageIndex);
				resolve();
            },
				error => { 
					console.log(error);
					resolve();
			});
		})
	}
}