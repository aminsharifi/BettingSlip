import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, computed } from '@angular/core';
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
  
  // ✅ All signals with proper types
  protected betslips = signal<Betslip[]>([]);
  protected isLoading = signal(true);
  protected errorMessage = signal('');
  
  // ✅ Computed signals
  protected betslipsCount = computed(() => this.betslips().length);
  protected totalStakes = computed(() => this.betslips().reduce((sum: number, b: Betslip) => sum + (b.stakeAmount || 0), 0));
  protected totalPotentialWin = computed(() => this.betslips().reduce((sum: number, b: Betslip) => sum + (b.potentialWin || 0), 0));

  ngOnInit(): void {
    this.loadBetslips();
  }

  private loadBetslips(): void {
    console.log('🔄 Loading betslips...');
    this.isLoading.set(true);
    this.errorMessage.set('');
    
    this.http.get<Betslip[]>('https://localhost:5002/api/betting-slips').subscribe({
      next: (response) => {
        if (Array.isArray(response)) {
          this.betslips.set(response);
          console.log('📡 API Response:', response);
        }
      },
      error: (error) => {
        console.error('❌ API Error:', error);
        this.errorMessage.set(`Failed to load: ${error.message}`);
      },
      complete: () => {
        this.isLoading.set(false);
      }
    });
  }

  protected editBetslip(id: string): void {
    console.log('Edit:', id);
  }

  protected placeBet(id: string): void {
    const currentBetslips = this.betslips();
    const index = currentBetslips.findIndex(b => b.id === id);
    if (index !== -1) {
      const updatedBetslip = { ...currentBetslips[index], status: 'Placed' as const };
      this.betslips.update(betslips => {
        const newArray = [...betslips];
        newArray[index] = updatedBetslip;
        return newArray;
      });
    }
  }

  protected refreshBetslips(): void {
    this.loadBetslips();
  }

  protected addNewBetslip(): void {
    const newId = 'draft-' + Date.now();
    const newBetslip: Betslip = {
      id: newId,
      stakeAmount: 100,
      totalOdds: 1,
      potentialWin: 100,
      status: "Draft",
      selections: []
    };
    this.betslips.update(betslips => [newBetslip, ...betslips]);
  }
}
