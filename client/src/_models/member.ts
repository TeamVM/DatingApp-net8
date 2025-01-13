import { Photo } from "./photo"

export interface Member {
    id: number
    userName: string
    age: number
    photoUlr: string
    knownAs: string
    created: Date
    lastActive: Date
    gender: string
    introduction: string
    lookingFor: string
    city: string
    country: string
    photos: Photo[]
  }
