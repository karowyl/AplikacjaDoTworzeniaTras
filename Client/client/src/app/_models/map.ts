import { Comment } from "./comment"

export interface Map {
    id?: number
    polyline?: string
    city?: string
    createdBy?: string;
    comments?: Comment[]
  }


  