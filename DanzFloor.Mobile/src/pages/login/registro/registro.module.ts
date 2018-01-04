import { NgModule } from '@angular/core';
import { IonicModule } from 'ionic-angular';

import { RegistroComponent } from "./registro.component";

@NgModule({
    declarations: [RegistroComponent],
    imports: [ IonicModule ],
    entryComponents: [RegistroComponent]
})
export class RegistroModule {}