export interface App {
  id: number;
  slug: string;
  displayText: string;
}

export type Result<TData> = {
  data?: TData;
  error?: any;
};
