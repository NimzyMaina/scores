import { FeedService } from './services/feed.service';
import { Component, OnInit } from '@angular/core';

import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [FeedService]
})
export class AppComponent implements OnInit {

  private connection: HubConnection;
  private builder: HubConnectionBuilder;

  constructor(private service: FeedService){}

  ngOnInit(): void {
    this.service.init();

    // this.service.start(true).subscribe(
    //   null,
    //   error => console.log('Error on init: ' + error));
    
    // this.builder = new HubConnectionBuilder();
    // this.connection = this.builder.withUrl("/broadcaster").build();

    // this.connection.on("setConnectionId",(id) =>{
    //   console.log('setconnid');
    //   console.log(id);
    // });

    // this.connection.on("UpdateMatch",(match:any) =>{
    //   console.log(match);
    // });

    // this.connection.start().catch(err => console.error(err.toString()));
  }
  title = 'app';
}
