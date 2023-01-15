import { Photo } from "./photo"

export interface Member {
    id: number
    username: string
    photoUrl: string
    country: string
    city: string
    createdDate: Date
    interests: string
    age: number
    photos: Photo[]
  }
  
