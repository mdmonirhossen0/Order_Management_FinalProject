import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { ItemComponent } from './Components/item/item.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DailyMenuComponent } from './Components/daily-menu/daily-menu.component';
import { CommonModule, DatePipe } from '@angular/common';
import { ReqviewComponent } from './Components/reqview/reqview.component';
import { SaleHeaderComponent } from './Components/sale-header/sale-header.component';
import { SaleheaderEditComponent } from './Components/saleheader-edit/saleheader-edit.component';

import { RecipeComponent } from './Components/recipe/recipe.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { TopsectionComponent } from './Components/topsection/topsection.component';
import { LeftWidgetComponent } from './Components/left-widget/left-widget.component';
import { RightWidgetComponent } from './Components/right-widget/right-widget.component';
import { BottomleftWidgetComponent } from './Components/bottomleft-widget/bottomleft-widget.component';
import { BottomRightWidgetComponent } from './Components/bottom-right-widget/bottom-right-widget.component';
import { RecipeEditComponent } from './Components/recipe-edit/recipe-edit.component';
import { ConfirmationDialogComponent } from './Components/confirmation-dialog/confirmation-dialog.component';
import { PatchDialogComponent } from './Components/patch-dialog/patch-dialog.component';

// Material Modules
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatMenuModule } from '@angular/material/menu';
import {MatDrawer, MatDrawerContainer, MatDrawerContent, MatSidenav} from '@angular/material/sidenav';
import { MatListModule, MatNavList } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { NgxPaginationModule } from 'ngx-pagination';

// Other Modules
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HighchartsChartModule } from 'highcharts-angular';
import { ChartModule } from 'angular-highcharts';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProcurementComponent } from './Components/procurement/procurement.component';
import { StockComponent } from './Components/stock/stock.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ItemComponent,
    DailyMenuComponent,
    ReqviewComponent,
    SaleHeaderComponent,
    SaleheaderEditComponent,
    RecipeComponent,
    DashboardComponent,
    TopsectionComponent,
    LeftWidgetComponent,
    RightWidgetComponent,
    BottomleftWidgetComponent,
    BottomRightWidgetComponent,
    RecipeEditComponent,
    ConfirmationDialogComponent,
    PatchDialogComponent,
    ProcurementComponent,
    StockComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatBadgeModule,
    MatSidenavModule,
    MatListModule,
    MatDrawerContainer,
    MatDrawer,
    MatNavList,
    MatDrawerContent,
    MatTabsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatFormFieldModule,
    BrowserAnimationsModule,
    MatInputModule,
    FontAwesomeModule,
    HighchartsChartModule,
    ChartModule,
    MatPaginatorModule,
    MatTableModule,
    NgxPaginationModule,
    ModalModule.forRoot()
  ],
  providers: [provideClientHydration(), provideAnimationsAsync(),provideHttpClient(),DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule {}
