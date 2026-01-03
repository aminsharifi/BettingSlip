import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = 'BettingSlip Angular Client ';
  ngOnInit(): void {
    this.http.get('https://localhost:8081/api/betting-slips')
    .subscribe(
      {
        next: response => console.log(response),
        error: error => console.error(error),
        complete: () => console.log('Completed the http request')
      }
    );
  }
  
}
