import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function getInitials(fullName: string): string {
  return fullName
    .split(' ')                  
    .filter(word => word)        
    .map(word => word[0].toUpperCase()) 
    .join('');
}
