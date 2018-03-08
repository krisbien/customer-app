import { DialogService } from './services/dialog.service';
import { CustomerService } from './services/customer.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { HttpClientModule } from '@angular/common/http';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressBarModule} from '@angular/material/progress-bar';

@NgModule({
  declarations: [
    AppComponent,
    CustomerListComponent,
    CustomerFormComponent
  ],
  imports: [
    BrowserModule,
    NoopAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatPaginatorModule,
    MatProgressBarModule
  ],
  providers: [CustomerService, DialogService],
  bootstrap: [AppComponent]
})
export class AppModule { }
