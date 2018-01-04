import { NgModule } from '@angular/core';
import { IonicModule } from 'ionic-angular';

import { LoginComponent } from "./login.component";
import { RegistroModule } from "./registro/registro.module";
import { LoginManualModule } from "./loginManual/loginManual.module";
@NgModule({
    declarations: [LoginComponent],
    imports: [ IonicModule, RegistroModule, LoginManualModule],
    entryComponents: [LoginComponent]
})
export class LoginModule {}