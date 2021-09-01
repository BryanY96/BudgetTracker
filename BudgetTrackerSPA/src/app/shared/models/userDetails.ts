export interface Income {
    id: number;
    userId: number;
    amount: number;
    description: string;
    incomeDate: Date;
    remarks: string;
}

export interface Expenditure {
    id: number;
    userId: number;
    amount: number;
    description: string;
    expDate: Date;
    remarks: string;
}

export interface User {
    id: number;
    fullname: string;
    email: string;
    joinedOn: Date;
    incomes: Income[];
    expenditures: Expenditure[];
    totalIncome: number;
    totalExpenditure: number;
    balance: number;
}