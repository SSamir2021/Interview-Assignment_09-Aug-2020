import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { HttpModule } from '@angular/http';
import { routing } from './app.routing';
import { EmployeeComponent } from './Components/employee.component';
import { HomeComponent } from './Components/home.component';
import { EmployeeService } from './Service/employee.service'

@NgModule({
    imports: [BrowserModule, ReactiveFormsModule, HttpModule, routing, Ng2Bs3ModalModule],
    declarations: [AppComponent, EmployeeComponent, HomeComponent],
    providers: [{ provide: APP_BASE_HREF, useValue: '/' }, EmployeeService],
    bootstrap: [AppComponent]

})
export class AppModule { }