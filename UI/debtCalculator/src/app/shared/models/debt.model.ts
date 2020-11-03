export class DebtAdd {
  clientCPF: string;
  value: number;
  dueDate: Date;
  contactPhone: string;
  maxSplit: number;
  interestType: number;
  interest: number;
  paschoalottoPercentage: number;
}


export class DebtConfig {
  id: number;
  maxSplit: number;
  interestType: number;
  interest: number;
  paschoalottoPercentage: number;
}

export class DebtList {
  id: number;
  clientCPF: string;
  value: number;
  dueDate: Date;
  contactPhone: string;
  maxSplit: number;
  interestType: number;
  interest: number;
  paschoalottoPercentage: number;
  finalized: boolean
}


export class DebtResponse {
  data: DebtList[];
  total: number
}

export class DebtInfo {
  DueDate: Date;
  DaysLate: number;
  OriginalValue: number;
  InterestValue: number;
  FinalValue: number;
  PlotsValue: Plots[];
  ContactPhone: string;
}

export class Plots {
  DueDate: Date;
  Value: number;
}

