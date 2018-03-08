
export class Customer {
  id: number;
  name: string;
  surname: string;
  telephone: string;
  address: string;
}

export class Page<T> {
  pageIndex: number;
  pageSzie: number;
  allCount: number;
  items: T[];
}

