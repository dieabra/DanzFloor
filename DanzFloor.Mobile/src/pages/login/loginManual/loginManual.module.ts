import { NgModule } from '@angular/core';
import { IonicModule } from 'ionic-angular';

import { LoginManualComponent } from "./loginManual.component";

@NgModule({
    declarations: [LoginManualComponent],
    imports: [ IonicModule ],
    entryComponents: [LoginManualComponent]
})
export class LoginManualModule {}