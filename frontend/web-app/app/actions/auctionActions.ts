'use server'

import { auth } from "@/auth";
import { fetchWrapper } from "@/lib/fetchWrapper";
import { Auction, Bid, PagedResult } from "@/types";
import { revalidatePath } from "next/cache";
import { FieldValues } from "react-hook-form";

export async function getData(query: string): Promise<PagedResult<Auction>> {
    return await fetchWrapper.get(`search${query}`)
  }

  export async function updateAuctionTest(){
    const data = {
      mileage: Math.floor(Math.random() * 10000) + 1
    }
    
    return await fetchWrapper.put('auctions/dc1e4071-d19d-459b-b848-b5c3cd3d151f', data);
  }

  export async function createAuction(data: FieldValues){
    return await fetchWrapper.post('auctions', data);
  }

  export async function getDetailedViewData(id: string): Promise<Auction>{
    return await fetchWrapper.get(`auctions/${id}`)
  }

  export async function updateAuction(data: FieldValues, id: string) {
    const res = fetchWrapper.put(`auctions/${id}`, data);
    revalidatePath(`/auctions/${id}`);
    return res;
  }

  export async function deleteAuction(id: string){
    return await fetchWrapper.del(`auctions/${id}`);
  }

  export async function getBidsForAuction(id: string): Promise<Bid[]> {
    return await fetchWrapper.get(`bids/${id}`);
  }

  export async function placeBidForAuction(auctionId: string, amount: number){
    return await fetchWrapper.post(`bids?auctionId=${auctionId}&amount=${amount}`, {})
  }