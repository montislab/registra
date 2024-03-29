import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CompaniesListComponent } from './administration/companies-list/companies-list.component';
import { UserManagementComponent } from './administration/user-management/user-management.component';
import { AuthGuard } from './_guards/auth.guard';
import { AuthService } from './services/auth.service';
import { AdminPanelComponent } from './administration/admin-panel/admin-panel.component';


const routes: Routes = [
  { path: '', component: HomeComponent },

  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin']}}
    ]
  },

  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
