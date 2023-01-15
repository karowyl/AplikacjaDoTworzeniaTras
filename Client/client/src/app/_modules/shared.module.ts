import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TimeagoCustomFormatter, TimeagoFormatter, TimeagoIntl, TimeagoModule } from 'ngx-timeago';
// import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { AlertModule } from 'ngx-bootstrap/alert';
import { CarouselModule } from 'ngx-bootstrap/carousel';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
    positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    TimeagoModule.forRoot({formatter: {provide: TimeagoFormatter, useClass: TimeagoCustomFormatter}}),
    // NgxGalleryModule,
    AlertModule.forRoot(),
    CarouselModule.forRoot(),
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    PaginationModule,
    ButtonsModule,
    TabsModule,
    // NgxGalleryModule,
    TimeagoModule,
    AlertModule
  ],
  providers: [
    TimeagoIntl,
  ]
})
export class SharedModule { }
