import { AuthService } from './services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { BsDropdownModule, TabsModule, ModalModule } from 'ngx-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { CompaniesListComponent } from './administration/companies-list/companies-list.component';
import { UserManagementComponent } from './administration/user-management/user-management.component';
import { AdminPanelComponent } from './administration/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { NotificationService } from './services/notification.service';
import { AdminService } from './services/admin.service';
import { RolesModalComponent } from './administration/roles-modal/roles-modal.component';
import { environment } from 'src/environments/environment';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      CompaniesListComponent,
      UserManagementComponent,
      AdminPanelComponent,
      HasRoleDirective,
      RolesModalComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: [environment.apiSocket],
            blacklistedRoutes: [environment.apiSocket + '/auth']
         }
      }),
      ModalModule.forRoot()
   ],
   providers: [
      AuthService,
      NotificationService,
      AdminService
   ],
   entryComponents: [
      RolesModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
