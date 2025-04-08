import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { DataComponent } from './components/data/data.component';
import { GreenhousesListComponent } from './components/greenhouses/greenhouses-list/greenhouses-list.component';
import { NotfoundpageComponent } from './components/notfoundpage/notfoundpage.component';
import { LoginComponent } from './components/login/login.component';
import { PlantsListComponent } from './components/plants/plants-list/plants-list.component';
import { authGuard } from './_guards/auth.guard';
import { SendResetEmailComponent } from './components/reset-password/send-reset-email/send-reset-email.component';
import { ConfirmCodeComponent } from './components/reset-password/confirm-code/confirm-code.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password/reset-password.component';
import { resetPasswordGuard } from './_guards/reset-password.guard';
import { notAuthGuard } from './_guards/not-auth.guard';
import { ValidateAccountComponent } from './components/validate-account/validate-account/validate-account.component';
import { ConfirmCodeValidateAccountComponent } from './components/validate-account/confirm-code-validate-account/confirm-code-validate-account.component';
import { validateAccountGuard } from './_guards/validate-account.guard';
import { CreateGreenhouseComponent } from './components/greenhouses/create-greenhouse/create-greenhouse.component';
import { EditGreenhouseComponent } from './components/greenhouses/edit-greenhouse/edit-greenhouse.component';
import { EditPlantComponent } from './components/plants/edit-plant/edit-plant.component';
import { CreatePlantComponent } from './components/plants/create-plant/create-plant.component';
import { SystemsListComponent } from './components/systems/systems-list/systems-list.component';
import { CreateSystemsComponent } from './components/systems/create-systems/create-systems.component';
import { EditSystemsComponent } from './components/systems/edit-systems/edit-systems.component';
import { ProcessesListComponent } from './components/processes/processes-list/processes-list.component';
import { CreateProcessComponent } from './components/processes/create-process/create-process.component';
import { EditProcessComponent } from './components/processes/edit-process/edit-process.component';
import { adminGuard } from './_guards/admin.guard';
import { AddingUserComponent } from './components/admin/adding-user/adding-user.component';
import { AddingSystemComponent } from './components/admin/adding-system/adding-system.component';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'validateaccount/confirmcode/:id', component: ConfirmCodeValidateAccountComponent},    
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [notAuthGuard],
    children: [
      {path: 'resetpassword', component: SendResetEmailComponent},
    ]
  },
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [validateAccountGuard],
    children: [
      {path: 'validateaccount/:id', component: ValidateAccountComponent}
    ]
  },
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [resetPasswordGuard],
    children: [
      {path: 'resetpassword/confirmcode', component: ConfirmCodeComponent},
      {path: 'resetpassword/confirmcode/changepassword/:id', component: ResetPasswordComponent}
    ]
  },
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {path: 'greenhouses', component: GreenhousesListComponent},
      {path: 'greenhouses/creategreenhouse', component: CreateGreenhouseComponent},
      {path: 'greenhouses/editgreenhouse/:id', component: EditGreenhouseComponent},
      {path: 'plants', component: PlantsListComponent},
      {path: 'plants/createplant', component: CreatePlantComponent},
      {path: 'plants/editplant/:id', component: EditPlantComponent},
      {path: 'systems', component: SystemsListComponent},
      {path: 'systems/createsystem', component: CreateSystemsComponent},
      {path: 'systems/editsystem/:id', component: EditSystemsComponent},
      {path: 'data', component: DataComponent},
      {path: 'processes', component: ProcessesListComponent},
      {path: 'processes/createprocess', component: CreateProcessComponent},
      {path: 'processes/editprocess/:id', component: EditProcessComponent},
    ]
  },
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [adminGuard],
    children: [
      {path: 'adduser', component: AddingUserComponent},
      {path: 'addsystem', component: AddingSystemComponent}
    ]
  },
  {path: '**', component: NotfoundpageComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
