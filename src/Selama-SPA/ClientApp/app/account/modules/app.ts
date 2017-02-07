import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "@angular/material";
import { UniversalModule } from "angular2-universal";

import { AccountRoutingModule } from "./routing";
import { AccountManagerModule } from "./manage/modules/app";

import { SignInGuard } from "../components/sign-in.guard";
import { RegisterComponent } from "../components/register";
import { SignInComponent } from "../components/sign-in";

@NgModule({
    imports: [
        UniversalModule,
        MaterialModule.forRoot(),
        AccountRoutingModule,
        AccountManagerModule,
        FormsModule,
    ],
    declarations: [
        RegisterComponent,
        SignInComponent,
    ],
    providers: [
        SignInGuard,
    ],
})
export class AccountModule
{
}