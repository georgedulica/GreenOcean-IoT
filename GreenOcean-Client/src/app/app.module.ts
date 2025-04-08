import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/login/login.component';
import { NavComponent } from './components/nav/nav.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DataComponent } from './components/data/data.component';
import { GreenhousesListComponent } from './components/greenhouses/greenhouses-list/greenhouses-list.component';
import { PlantsListComponent } from './components/plants/plants-list/plants-list.component';
import { HomeComponent } from './components/home/home.component';
import { NotfoundpageComponent } from './components/notfoundpage/notfoundpage.component';
import { SendResetEmailComponent } from './components/reset-password/send-reset-email/send-reset-email.component';
import { ConfirmCodeComponent } from './components/reset-password/confirm-code/confirm-code.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password/reset-password.component';
import { ValidateAccountComponent } from './components/validate-account/validate-account/validate-account.component';
import { ConfirmCodeValidateAccountComponent } from './components/validate-account/confirm-code-validate-account/confirm-code-validate-account.component';
import { CreateGreenhouseComponent } from './components/greenhouses/create-greenhouse/create-greenhouse.component';
import { EditGreenhouseComponent } from './components/greenhouses/edit-greenhouse/edit-greenhouse.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { CreatePlantComponent } from './components/plants/create-plant/create-plant.component';
import { EditPlantComponent } from './components/plants/edit-plant/edit-plant.component';
import { SystemsListComponent } from './components/systems/systems-list/systems-list.component';
import { EditSystemsComponent } from './components/systems/edit-systems/edit-systems.component';
import { CreateSystemsComponent } from './components/systems/create-systems/create-systems.component';
import { ProcessesListComponent } from './components/processes/processes-list/processes-list.component';
import { CreateProcessComponent } from './components/processes/create-process/create-process.component';
import { EditProcessComponent } from './components/processes/edit-process/edit-process.component';
import { RoleDirective } from './_directives/role.directive';
import { AddingUserComponent } from './components/admin/adding-user/adding-user.component';
import { AddingSystemComponent } from './components/admin/adding-system/adding-system.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavComponent,
    DataComponent,
    GreenhousesListComponent,
    PlantsListComponent,
    HomeComponent,
    NotfoundpageComponent,
    SendResetEmailComponent,
    ConfirmCodeComponent,
    ResetPasswordComponent,
    ValidateAccountComponent,
    ConfirmCodeValidateAccountComponent,
    CreateGreenhouseComponent,
    EditGreenhouseComponent,
    CreatePlantComponent,
    EditPlantComponent,
    SystemsListComponent,
    EditSystemsComponent,
    CreateSystemsComponent,
    ProcessesListComponent,
    CreateProcessComponent,
    EditProcessComponent,
    RoleDirective,
    AddingUserComponent,
    AddingSystemComponent
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
