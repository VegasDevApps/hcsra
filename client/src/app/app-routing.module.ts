import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { InsurancePolicyFormComponent } from './insurance-policy-form/insurance-policy-form.component';

const routes: Routes = [
  {path: '', component: UserListComponent},
  {path: 'user-details', component: UserDetailsComponent},
  {path: 'insurance-policy', component: InsurancePolicyFormComponent},
  {path: '**', redirectTo: ''},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
