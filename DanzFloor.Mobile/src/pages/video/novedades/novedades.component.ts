import { Component, OnInit } from '@angular/core';
import { NavController } from 'ionic-angular';
import { EntornoConfig } from "../../../constants/entorno.config";
import { VimeoService } from '../../../services/vimeo.service';
import { EpisodioService } from '../../../services/episodio.service';
import { ProgramaService } from '../../../services/programa.service';
import { VideoItem } from '../videoItem/videoItem.component'
import { SharedService } from "../../../services/shared.service";
import { SocialSharing } from '@ionic-native/social-sharing';
import { CanalItem } from '../canalItem/canalItem.component';
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { Platform } from 'ionic-angular';

@Component({
	selector: 'novedades',
	templateUrl: './novedades.component.html',
	providers: [VimeoService, EpisodioService, SharedService, EntornoConfig, ProgramaService]
})
export class NovedadesComponent implements OnInit {
	private service: VimeoService;
	private nav: NavController;
	private sharedService: SharedService;
	public episodios: any[] = [];
	private Pages:any = null;
	private pageIndex: any = 1;
	private pageSize: any = 3;

	constructor(service: VimeoService, nav: NavController,
		private episodioService: EpisodioService,
		private socialSharing: SocialSharing,
		private entornoConfig: EntornoConfig,
		private _sharedService: SharedService,
		private programaService: ProgramaService,
        private platform: Platform,
		private ga: GoogleAnalytics) {
		
			this.service = service;
			this.nav = nav;
			
			platform.ready().then(() => {
              // Okay, so the platform is ready and our plugins are available.
              // Here you can do any higher level native things you might need

              platform.registerBackButtonAction(() => {
				if(this.nav.canGoBack()){
                  this.nav.pop();
                }
              });
            });

	}

	ngOnInit(): void {
		this.ga.trackView('View: Novedades');
		this.episodioService.Novedades(this.pageIndex, this.pageSize)
			.subscribe(respuesta => {
				this.episodios = respuesta.Result;
			},
			error => { console.log(error) });
			this.doRefresh("");
	}

	ObtenerImagen(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-256|width-453|mode-crop|quality-100|';
	}
	
	ObtenerImagenPrograma(imagen): any {
		return this._sharedService.Core + "/archivo/ObtenerImagenPorId/" + imagen + '|height-40|width-40|mode-crop|quality-100|';
	}

	public itemTapped(item) {
		this.nav.push(VideoItem, {
			item: item
		});
	}

	public share(episodio: any) {
		 this.ga.trackEvent('Novedades', 'Share');
		this.socialSharing.shareWithOptions({ url: this._sharedService.Core + '/FrontEnd/Reproducir/' + episodio.Id })
	}

	public itemTappedPrograma(item) {
		this.nav.push(CanalItem, {
			item: item
		});
	}

	like(item) {
		this.ga.trackEvent('Novedades', 'Like');
		item.Like = !item.Like;

		if (item.Like)
			item.CantidadLikes = item.CantidadLikes + 1;
		else
			item.CantidadLikes = item.CantidadLikes - 1;

		this.episodioService.Like(item.Id, item.Like)
			.subscribe(respuesta => {
			},
			error => { console.log(error) });
	}

	bookmark(item) {
		this.ga.trackEvent('Novedades', 'Bookmark');
		item.Bookmark = !item.Bookmark;

		this.episodioService.Bookmark(item.Id, item.Bookmark)
			.subscribe(respuesta => {

			},
			error => { console.log(error) });
	}

	doInfinite(): Promise<any> {
		
		return new Promise((resolve) => {

			this.pageIndex = this.pageIndex + 1;
			
			if(this.Pages != null && this.pageIndex > this.Pages)
				{
					resolve();
					return;
				}

			this.episodioService.Novedades(this.pageIndex, this.pageSize)
				.subscribe(respuesta => {
					this.ga.trackEvent('Novedades', 'ObtenerMasNovedades');
					for (var i = 0; i < respuesta.Result.length; i++) {
						this.episodios.push(respuesta.Result[i]);
					}
					console.log('Finaliza la carga ' + this.pageIndex);
					resolve();
				},
				error => { 
					console.log(error);
					//resolve();
				 });

		})
	}

	cargarData() {
		this.pageIndex = 1;
		this.episodioService.Novedades(this.pageIndex, this.pageSize)
			.subscribe(respuesta => {
				this.episodios = respuesta.Result;
			},
			error => { 
				console.log(error);
				});
	}


	doRefresh(refresher) {
		this.ga.trackEvent('Bookmark', 'Refresh');
		this.pageIndex = 1;
		this.episodioService.Novedades(this.pageIndex, this.pageSize)
			.subscribe(respuesta => {
				this.episodios = respuesta.Result;
				this.Pages = respuesta.Pages;
				try{refresher.complete();}catch(a){}
			},
			error => { 
				console.log(error);
					try{refresher.complete();}catch(a){}
				});
	}

	color(programa): any{
		return programa.EsFavorito == true ? 'primary' : 'dark';		
	}

	
}