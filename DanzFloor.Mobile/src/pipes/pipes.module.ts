import { NgModule } from '@angular/core';
import { DatePipe } from '@angular/common';
/*import { TrimHtmlPipe } from './trim-html.pipe';*/
import { TruncatePipe } from './truncate.pipe';
import { GroupByPipe } from './groupBy.pipe';
import { VimeoEmbedUrlPipe } from './videoEmbebedUrl.pipe'
import { MvcDatePipe } from './mvcDatePipe.pipe'
/*
import { YoutubeEmbedUrlPipe } from './youtube-embed-url.pipe';
import { VimeoEmbedUrlPipe } from './vimeo-embed-url.pipe';
import { LocalDatePipe } from './local-date.pipe';*/

@NgModule({
	declarations: [
		TruncatePipe,
        GroupByPipe,
        VimeoEmbedUrlPipe,
		MvcDatePipe/*,
		TrimHtmlPipe,
		YoutubeEmbedUrlPipe,
		VimeoEmbedUrlPipe,
		LocalDatePipe*/
	],
	exports: [
		TruncatePipe,
        GroupByPipe,
        VimeoEmbedUrlPipe,
		MvcDatePipe/*,
		TrimHtmlPipe,
		YoutubeEmbedUrlPipe,
		VimeoEmbedUrlPipe,
		LocalDatePipe*/
	],
	providers: [DatePipe]
})
export class PipesModule {

}