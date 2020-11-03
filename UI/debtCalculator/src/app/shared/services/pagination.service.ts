import { Component, OnChanges, SimpleChanges, EventEmitter, Input, Output } from '@angular/core';
import { faBackward, faCaretLeft, faCaretRight, faForward } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'pagination-component',
  template: `<span class="pull-center" style="align-items:center; display:inline-flex">
    <button class="btn btn-secondary btn-sm" (click)="primeira()" [disabled]="paginaAtual == 1 || totalPaginas == 0" style="margin-right:3px;">
      <fa-icon [icon]="faBackward"></fa-icon>
    </button>
    <button class="btn btn-secondary btn-sm" (click)="anterior()" [disabled]="paginaAtual == 1 || totalPaginas == 0" style="margin-right:3px;">
      <fa-icon [icon]="faCaretLeft"></fa-icon>
    </button>
    <!-- <strong><input #page class="form-control-sm" placeholder={{paginaAtual}} style="width: 25px" (keyup)="alterar(page.value)"/>/{{totalPaginas}}</strong> -->
    <strong>{{paginaAtual}}/{{totalPaginas}}</strong>
    &nbsp;
    <button class="btn btn-secondary btn-sm" (click)="proximo()" [disabled]="paginaAtual == totalPaginas || totalPaginas == 0" style="margin-right:3px;">
      <fa-icon [icon]="faCaretRight"></fa-icon>
    </button>
    <button class="btn btn-secondary btn-sm" (click)="ultima()" [disabled]="paginaAtual == totalPaginas || totalPaginas == 0" style="margin-right:3px;">
      <fa-icon [icon]="faForward"></fa-icon>
    </button>
    <select style="border: 1px solid rgba(0,0,0,.15); border-radius: .25rem; font-weight: bold !important;" title="Alterar número de itens para exibição" [hidden]="!exibeSelecaoItens" [(ngModel)]="itensPagina"
      (change)="alterarItensPagina()" style="margin-left:3px; padding:2px;" required>
      <option [ngValue]="10">10</option>
      <option [ngValue]="25">25</option>
      <option [ngValue]="50">50</option>
    </select>
  </span>`
})

export class PaginationComponent implements OnChanges {
  ngOnChanges(changes: SimpleChanges): void {
    this.reiniciarPaginacao();
  }

  @Input() totalItens: number;
  @Input() exibeSelecaoItens: boolean;

  @Output() carregarItens: EventEmitter<any> = new EventEmitter();

  totalPaginas: number = 0;
  itensPagina: number = 0;
  paginaAtual: number = 0;

  faBackward = faBackward;
  faCaretLeft = faCaretLeft;
  faForward = faForward;
  faCaretRight = faCaretRight;


  proximo() {
    let pAtual = this.paginaAtual + 1;
    if (pAtual <= this.totalPaginas) {
      this.paginaAtual = pAtual;
      this.acionarCarregamento();
    }
  }

  anterior() {
    let pAtual = this.paginaAtual - 1;
    if (pAtual >= 1) {
      this.paginaAtual = pAtual;
      this.acionarCarregamento();
    }
  }

  alterarItensPagina() {
    this.paginaAtual = 1;
    this.totalPaginas = Math.ceil(this.totalItens / this.itensPagina);
    this.acionarCarregamento();
  }

  reiniciarPaginacao() {
    this.paginaAtual = 1;
    this.itensPagina = 10;
    this.totalPaginas = Math.ceil(this.totalItens / this.itensPagina);
  }

  ajustarPaginacao(totalItens: number) {
    this.totalItens = totalItens;
    this.totalPaginas = Math.ceil(this.totalItens / this.itensPagina);
    if (this.totalPaginas < this.paginaAtual) {
      let pAtual = this.paginaAtual - 1;
      if (pAtual >= 1) {
        this.paginaAtual = pAtual;
      }
    }
    this.acionarCarregamento();
  }

  private acionarCarregamento() {
    let skip = (this.paginaAtual - 1) * this.itensPagina;
    //chama o método indicado na diretiva carregarItens do component
    this.carregarItens.emit({ skip: skip, take: this.itensPagina });
  }

  primeira() {
    this.paginaAtual = 1;
    let pAtual = this.paginaAtual;
    if (pAtual >= 1) {
      this.paginaAtual = pAtual;
      this.acionarCarregamento();
    }
  }

  ultima() {
    this.paginaAtual = this.totalPaginas;
    let pAtual = this.paginaAtual;
    if (pAtual <= this.totalPaginas) {
      this.paginaAtual = pAtual;
      this.acionarCarregamento();
    }
  }

  alterar(pagina: number) {
    if (pagina) {
      if (pagina <= this.totalPaginas) {
        this.paginaAtual = pagina;
        this.acionarCarregamento();
      }
    } else {
      this.paginaAtual = 1;
      this.acionarCarregamento();
    }
  }
}
