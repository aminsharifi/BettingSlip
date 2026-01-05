import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule, SlicePipe, CurrencyPipe, DecimalPipe } from '@angular/common';
import { lastValueFrom } from 'rxjs';

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

  private async loadBetslips() {
    console.log('🔄 Loading betslips...');
    this.isLoading.set(true);
    this.errorMessage.set('');
    await this.getbetslips();
    console.log('🔄 Loading Complete');
  }

  private async getbetslips()
  {
    try
    {
      var result = await lastValueFrom(this.http.get<Betslip[]>('https://localhost:5002/api/betting-slips'));
      this.betslips.set(result);
      this.isLoading.set(false);
      return;
    }
    catch(error)
    {
      console.log(error);
      throw error;
    }
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
