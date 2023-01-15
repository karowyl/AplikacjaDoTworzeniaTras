import { Component, EventEmitter, NgModule, OnInit, Output } from '@angular/core';
import * as L from 'leaflet';
import 'leaflet-routing-machine';
import { ToastrService } from 'ngx-toastr';
import { empty } from 'rxjs';
import { Map } from 'src/app/_models/map';
import { Comment } from 'src/app/_models/comment';
import { MapsService } from 'src/app/_services/maps.service';
import { DatePipe } from '@angular/common';
import { User } from 'src/app/_models/user';


@Component({
  selector: 'app-maps-list',
  templateUrl: './maps-list.component.html',
  styleUrls: ['./maps-list.component.css']
})

export class MapsListComponent implements OnInit {
  coordinates: any[] = []; 
  maps: Map[];
  route: Map;
  search: string;
  city: string;
  polyline: any;
  marker: L.Marker;
  myIcon: L.Icon;
  previousPolyline: any;
  selectedMap: Map;
  newComment: string;
  searchByUsername: string;
  constructor(private mapService: MapsService, private toastr: ToastrService) { }
  
  myMap : L.Map ;
  myMap2 : L.Map;
  myMap3 : L.Map;

  ngOnInit(): void {
    this.myMap = L.map('map').setView([51.505, -0.09], 13);
    this.myMap2 = L.map('map2').setView([51.505, -0.09], 13);
    this.myMap3 = L.map('map3').setView([51.505, -0.09], 13);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
  }).addTo(this.myMap);

   
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
  }).addTo(this.myMap2);  

  
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
  }).addTo(this.myMap3);
  this.myIcon = L.icon({
    iconUrl: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAApCAYAAADAk4LOAAAFgUlEQVR4Aa1XA5BjWRTN2oW17d3YaZtr2962HUzbDNpjszW24mRt28p47v7zq/bXZtrp/lWnXr337j3nPCe85NcypgSFdugCpW5YoDAMRaIMqRi6aKq5E3YqDQO3qAwjVWrD8Ncq/RBpykd8oZUb/kaJutow8r1aP9II0WmLKLIsJyv1w/kqw9Ch2MYdB++12Onxee/QMwvf4/Dk/Lfp/i4nxTXtOoQ4pW5Aj7wpici1A9erdAN2OH64x8OSP9j3Ft3b7aWkTg/Fm91siTra0f9on5sQr9INejH6CUUUpavjFNq1B+Oadhxmnfa8RfEmN8VNAsQhPqF55xHkMzz3jSmChWU6f7/XZKNH+9+hBLOHYozuKQPxyMPUKkrX/K0uWnfFaJGS1QPRtZsOPtr3NsW0uyh6NNCOkU3Yz+bXbT3I8G3xE5EXLXtCXbbqwCO9zPQYPRTZ5vIDXD7U+w7rFDEoUUf7ibHIR4y6bLVPXrz8JVZEql13trxwue/uDivd3fkWRbS6/IA2bID4uk0UpF1N8qLlbBlXs4Ee7HLTfV1j54APvODnSfOWBqtKVvjgLKzF5YdEk5ewRkGlK0i33Eofffc7HT56jD7/6U+qH3Cx7SBLNntH5YIPvODnyfIXZYRVDPqgHtLs5ABHD3YzLuespb7t79FY34DjMwrVrcTuwlT55YMPvOBnRrJ4VXTdNnYug5ucHLBjEpt30701A3Ts+HEa73u6dT3FNWwflY86eMHPk+Yu+i6pzUpRrW7SNDg5JHR4KapmM5Wv2E8Tfcb1HoqqHMHU+uWDD7zg54mz5/2BSnizi9T1Dg4QQXLToGNCkb6tb1NU+QAlGr1++eADrzhn/u8Q2YZhQVlZ5+CAOtqfbhmaUCS1ezNFVm2imDbPmPng5wmz+gwh+oHDce0eUtQ6OGDIyR0uUhUsoO3vfDmmgOezH0mZN59x7MBi++WDL1g/eEiU3avlidO671bkLfwbw5XV2P8Pzo0ydy4t2/0eu33xYSOMOD8hTf4CrBtGMSoXfPLchX+J0ruSePw3LZeK0juPJbYzrhkH0io7B3k164hiGvawhOKMLkrQLyVpZg8rHFW7E2uHOL888IBPlNZ1FPzstSJM694fWr6RwpvcJK60+0HCILTBzZLFNdtAzJaohze60T8qBzyh5ZuOg5e7uwQppofEmf2++DYvmySqGBuKaicF1blQjhuHdvCIMvp8whTTfZzI7RldpwtSzL+F1+wkdZ2TBOW2gIF88PBTzD/gpeREAMEbxnJcaJHNHrpzji0gQCS6hdkEeYt9DF/2qPcEC8RM28Hwmr3sdNyht00byAut2k3gufWNtgtOEOFGUwcXWNDbdNbpgBGxEvKkOQsxivJx33iow0Vw5S6SVTrpVq11ysA2Rp7gTfPfktc6zhtXBBC+adRLshf6sG2RfHPZ5EAc4sVZ83yCN00Fk/4kggu40ZTvIEm5g24qtU4KjBrx/BTTH8ifVASAG7gKrnWxJDcU7x8X6Ecczhm3o6YicvsLXWfh3Ch1W0k8x0nXF+0fFxgt4phz8QvypiwCCFKMqXCnqXExjq10beH+UUA7+nG6mdG/Pu0f3LgFcGrl2s0kNNjpmoJ9o4B29CMO8dMT4Q5ox8uitF6fqsrJOr8qnwNbRzv6hSnG5wP+64C7h9lp30hKNtKdWjtdkbuPA19nJ7Tz3zR/ibgARbhb4AlhavcBebmTHcFl2fvYEnW0ox9xMxKBS8btJ+KiEbq9zA4RthQXDhPa0T9TEe69gWupwc6uBUphquXgf+/FrIjweHQS4/pduMe5ERUMHUd9xv8ZR98CxkS4F2n3EUrUZ10EYNw7BWm9x1GiPssi3GgiGRDKWRYZfXlON+dfNbM+GgIwYdwAAAAASUVORK5CYII='
  })
  this.myMap.on('click', (e) => this.createPoint(e));
  this.getAllRoutes();
  this.getMyRoutes();
  }
 
  createPoint(e) {

    const point: any[] = [e.latlng.lat, e.latlng.lng]; 
    this.coordinates.push(point);
    if(this.coordinates.length > 1)
    {
      this.createPath();
    }
    else{
      this.marker = L.marker(e.latlng, {icon: this.myIcon}).addTo(this.myMap)
      .bindPopup('This route is made by: Karol')
      .openPopup();
    }
}

getAllRoutes() {
  this.mapService.getAllRoutes().subscribe(maps => {
    this.maps = maps;
    
    this.maps.forEach(map => {
      var finalCoordinates: any[] = [];
      var tempCoordinates = map.polyline.split(',');
      for (let i = 0; i < tempCoordinates.length; i = i+2) {
        const point: any[] = [tempCoordinates[i], tempCoordinates[i+1]]; 
        finalCoordinates.push(point);
      }
      L.polyline(finalCoordinates, {color: 'red'}).addTo(this.myMap2);
      L.marker(finalCoordinates[0], {icon: this.myIcon})
      .on('click',  (e) => this.showComments(e, map)).addTo(this.myMap2)
      .bindPopup('Ta trasa jest stworzona przez: ' + map.createdBy)
      .openPopup();
    }
    )
})
}
getMyRoutes() {
  this.mapService.getMyRoutes().subscribe(maps => {
    this.maps = maps;
    
    this.maps.forEach(map => {
      var finalCoordinates: any[] = [];
      var tempCoordinates = map.polyline.split(',');
      for (let i = 0; i < tempCoordinates.length; i = i+2) {
        const point: any[] = [tempCoordinates[i], tempCoordinates[i+1]]; 
        finalCoordinates.push(point);
      }
      L.polyline(finalCoordinates, {color: 'red'}).addTo(this.myMap3);
      L.marker(finalCoordinates[0], {icon: this.myIcon}).addTo(this.myMap3)
      .bindPopup('This route is made by: ' + map.createdBy)
      .openPopup();
    }
    )
})
}


createPath() {

  this.polyline = L.polyline(this.coordinates, {color: 'red'}).addTo(this.myMap);

  if(this.previousPolyline === null || this.previousPolyline === undefined)
    this.previousPolyline = this.polyline;
  else
  {
    this.myMap.removeLayer(this.previousPolyline);
    this.previousPolyline = this.polyline;
  }
 }

 showComments(e: any, map: Map) 
 {
    this.selectedMap = map;
 }

 findRoutes() {
  if(this.search != null && this.search != undefined && this.search != "" ) {
    
    this.mapService.getRoutesInTheCity(this.search).subscribe(maps => {
      if(maps.length > 0)
      {
        this.maps = maps;
        this.myMap2.eachLayer((layer) => {
          layer.remove()
        });
        this.maps.forEach(map => {
          
          var finalCoordinates: any[] = [];
          var tempCoordinates = map.polyline.split(',');
          for (let i = 0; i < tempCoordinates.length; i = i+2) {
            const point: any[] = [tempCoordinates[i], tempCoordinates[i+1]]; 
            finalCoordinates.push(point);
          }
          L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
          maxZoom: 19,
          attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
          }).addTo(this.myMap2)
          
          L.polyline(finalCoordinates, {color: 'red'}).addTo(this.myMap2);
          L.marker(finalCoordinates[0], {icon: this.myIcon}).addTo(this.myMap2)
          .bindPopup('This route is made by: ' + map.createdBy)
          .openPopup();
        }
        )
      }
      else {
        this.toastr.info("Jeszcze nie istnieje żadna trasa w tym mieście");
      }
  })
}
else {
  this.toastr.info("Proszę, wypełnij pole z miastem");
}
 }

 findRoutesByUser() {
  if(this.searchByUsername != null && this.searchByUsername != undefined && this.searchByUsername != "" ) {
    this.mapService.getRoutesCreatedByUser(this.searchByUsername).subscribe(maps => {
      console.log(this.searchByUsername)
      if(maps.length > 0)
      {
        this.maps = maps;
        this.myMap2.eachLayer((layer) => {
          layer.remove()
        });
        this.maps.forEach(map => {
          
          var finalCoordinates: any[] = [];
          var tempCoordinates = map.polyline.split(',');
          for (let i = 0; i < tempCoordinates.length; i = i+2) {
            const point: any[] = [tempCoordinates[i], tempCoordinates[i+1]]; 
            finalCoordinates.push(point);
          }
          L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
          maxZoom: 19,
          attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
          }).addTo(this.myMap2)
          
          L.polyline(finalCoordinates, {color: 'red'}).addTo(this.myMap2);
          L.marker(finalCoordinates[0], {icon: this.myIcon}).addTo(this.myMap2)
          .bindPopup('This route is made by: ' + map.createdBy)
          .openPopup();
        }
        )
      }
      else {
        this.toastr.info("Jeszcze nie istnieje żadna trasa stworzona przez tego użytkownika");
      }
  })
}
else {
  this.toastr.info("Proszę, wypełnij pole z nazwą użytkownika");
}
 }

 save(){
  var map : Map = this.prepareMap();
  if (map.city === null || map.city === undefined) {
    this.toastr.info("Proszę, nie zapomnij o mieście");
  }
  if (this.coordinates.length <= 1) {
    this.toastr.info("Proszę, najpierw stwórz droge");
  }
  if(this.coordinates.length > 1 && map.city != null)
  {
    this.mapService.addRoute(map).subscribe(response => {
      this.toastr.success("Pomyślnie dodano trasę");
      this.cancel();
      this.getAllRoutes();
      this.getMyRoutes();
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    })
  }
}

back() {
  if(this.coordinates.length === 0) {
    this.toastr.info("Nie masz żadnych zmian do usunięcia");
  }
  if (this.coordinates.length > 2) {
    this.coordinates = this.coordinates.slice(0, -1);
    this.previousPolyline = this.coordinates.slice(0, -1);
    this.myMap.removeLayer(this.polyline);
    this.polyline = L.polyline(this.coordinates, {color: 'red'}).addTo(this.myMap);
  } 
  else {
    this.coordinates = [];
    this.previousPolyline = [];
    if(this.polyline != null || this.polyline != undefined) {
      this.myMap.removeLayer(this.polyline);
      this.myMap.removeLayer(this.marker);
    }
    
  }
  

}
prepareMap() : Map
{
  return { 
    city : this.city,
    polyline : this.coordinates.toString()}
}

cancel(){
  this.coordinates = [];
  if(this.polyline != undefined)
  {
    this.myMap.removeLayer(this.polyline);
  }
  if(this.marker != undefined)
  {
    this.myMap.removeLayer(this.marker);
  }
  
}

prepareComment() : Comment
{
  const user = JSON.parse(localStorage.getItem("user")) as User;
  return { 
    context : this.newComment,
    mapId : this.selectedMap.id,
    createdBy : user.username,
    createdDate : new Date()
  }
}

addComment()
{
  var comment : Comment = this.prepareComment();
  if (this.newComment === null || this.newComment === undefined || this.newComment === "") {
      this.toastr.info("Najpierw napisz komenatrz");
    }
  else
  {
    this.mapService.addComment(comment).subscribe(response => {
    this.toastr.success("Pomyślnie dodano komentarz");
    this.selectedMap.comments.push(comment)
  },error => {
        console.log(error);
        this.toastr.error(error.error);
      })
   }
}
}
