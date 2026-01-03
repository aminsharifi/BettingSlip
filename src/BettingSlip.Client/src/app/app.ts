import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, computed, effect } from '@angular/core';
import { CommonModule, SlicePipe, CurrencyPipe, DecimalPipe } from '@angular/common';

interface Selection {
  id: string;
  eventName: string;
  market: string;
  odd: number;
}

interface Betslip {
  id: string;
  stakeAmount: number;
  totalOdds: number;
  potentialWin: number;
  status: string;
  selections?: Selection[];
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, SlicePipe, CurrencyPipe, DecimalPipe],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = 'BettingSlip Angular Client';
  protected betslips: Betslip[] = [];
  protected isLoading = true;
  protected errorMessage = '';
  protected totalPotentialWin = signal(0);
  protected totalStakes = signal(0);
  protected betslipsCount = computed(() => this.betslips.length);

  private demoData: Betslip[] = [
    {
      id: "demo-1",
      stakeAmount: 100,
      totalOdds: 1.85,
      potentialWin: 185,
      status: "Draft",
      selections: [{ id: "sel-1", eventName: "Real Madrid vs Barcelona", market: "Match Winner", odd: 1.85 }]
    },
    {
      id: "demo-2", 
      stakeAmount: 50,
      totalOdds: 2.1,
      potentialWin: 105,
      status: "Draft",
      selections: []
    }
  ];

  constructor() {
    effect(() => this.updateTotals());
  }

  ngOnInit(): void {
    this.betslips = [...this.demoData];
    this.isLoading = false;
    setTimeout(() => this.loadBetslips(), 500);
  }

  private loadBetslips(): void {
    console.log('🔄 Loading betslips...');
    this.isLoading = true;
    this.errorMessage = '';
    this.http.get<any[]>('https://localhost:5002/api/betting-slips').subscribe({
      next: (response) => {
        console.log('📡 API Response:', response);
        if (Array.isArray(response) && response.length > 0) {
          this.betslips = response;
        }
      },
      error: (error) => {
        console.error('❌ API Error:', error);
        this.errorMessage = `Failed to load: ${error.message}`;
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  private updateTotals(): void {
    const stakes = this.betslips.reduce((sum, b) => sum + (b.stakeAmount || 0), 0);
    const wins = this.betslips.reduce((sum, b) => sum + (b.potentialWin || 0), 0);
    this.totalStakes.set(stakes);
    this.totalPotentialWin.set(wins);
  }

  protected editBetslip(id: string): void {
    console.log('Edit:', id);
  }

  protected placeBet(id: string): void {
    const index = this.betslips.findIndex(b => b.id === id);
    if (index !== -1) {
      this.betslips[index] = { ...this.betslips[index], status: 'Placed' as const };
    }
  }

  protected refreshBetslips(): void {
    this.loadBetslips();
  }

  protected addNewBetslip(): void {
    const newId = 'draft-' + Date.now();
    this.betslips.unshift({
      id: newId,
      stakeAmount: 100,
      totalOdds: 1,
      potentialWin: 100,
      status: "Draft",
      selections: []
    });
  }
}
