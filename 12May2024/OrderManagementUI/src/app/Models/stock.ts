import { Item } from "./item";

export interface Stock {
    id: number,
    itemId: number,
    item: Item,
    quantity: number
}
