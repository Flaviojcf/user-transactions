import { z } from "zod";

export const createUserSchema = z.object({
  fullName: z.string().min(1, "Nome completo é obrigatório"),
  email: z
    .string()
    .min(1, "Email é obrigatório")
    .email("Formato de email inválido"),
  cpf: z
    .string()
    .min(1, "CPF é obrigatório")
    .regex(
      /^(\d{3})[\.\s]?(\d{3})[\.\s]?(\d{3})[-\s]?(\d{2})$/,
      "Formato de CPF inválido"
    ),
  password: z
    .string()
    .min(1, "Senha é obrigatória")
    .min(6, "Senha deve ter pelo menos 6 caracteres"),
  userType: z.string().min(1, "Tipo de usuário é obrigatório"),
});

export type CreateUserFormData = z.infer<typeof createUserSchema>;

export const createWalletSchema = z.object({
  userId: z.string().min(1, "ID do usuário é obrigatório"),
});

export type CreateWalletFormData = z.infer<typeof createWalletSchema>;

export const createTransactionSchema = z
  .object({
    amount: z
      .number("Valor deve ser um número" )
      .positive("Valor deve ser maior que zero"),
    senderId: z.string().min(1, "Remetente é obrigatório"),
    receiverId: z.string().min(1, "Destinatário é obrigatório"),
  })
  .refine((data) => data.senderId !== data.receiverId, {
    message: "Remetente e destinatário devem ser diferentes",
    path: ["receiverId"],
  });

export type CreateTransactionFormData = z.infer<typeof createTransactionSchema>;
