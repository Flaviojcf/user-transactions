import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"
import { toast } from "sonner"
import { ApiErrorResponse } from "./types"

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

export function handleApiError(error: ApiErrorResponse) {
  if (error && error.errorDetails && error.errorDetails.messages) {
    toast.error(error.errorDetails.messages[0]);
  } 
}

export function showSuccessMessage(message: string) {
  toast.success(message);
}

export function showInfoMessage(message: string) {
  toast.info(message);
}

export function showWarningMessage(message: string) {
  toast.warning(message);
}
