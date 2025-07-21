import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@/components/ui/select";
import { TabsContent } from "@/components/ui/tabs";
import { ArrowRightLeft, CheckCircle, Wallet } from "lucide-react";

export default function TabWallet() {
  const users = [
    {
      id: "1",
      name: "João Silva",
      email: "joao@example.com",
      type: "User",
      cpf: "123.456.789-01",
      balance: 750.0,
    },
    {
      id: "2",
      name: "Maria Santos",
      email: "maria@example.com",
      type: "Merchant",
      cpf: "987.654.321-09",
      balance: 1250.0,
    },
    {
      id: "3",
      name: "Pedro Costa",
      email: "pedro@example.com",
      type: "User",
      cpf: "456.789.123-45",
      balance: 320.5,
    },
  ];

  return (
    <TabsContent value="wallets" className="space-y-6">
      <div className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Cadastrar Nova Carteira</CardTitle>
            <CardDescription>
              Criar carteira digital para usuário
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form
              onSubmit={(e) => {
                e.preventDefault();
                console.log(
                  "Wallet registration for user:",
                  e.currentTarget.userId.value
                );
              }}
              className="space-y-4"
            >
              <div className="space-y-2">
                <Label htmlFor="userId">Selecionar Usuário</Label>
                <Select name="userId" required>
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione um usuário" />
                  </SelectTrigger>
                  <SelectContent>
                    {users.map((user) => (
                      <SelectItem key={user.id} value={user.id}>
                        {user.name} - {user.email}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div className="space-y-2">
                <Label>Saldo Inicial</Label>
                <div className="p-3 bg-gray-50 rounded-lg border">
                  <div className="flex items-center justify-between">
                    <span className="text-sm text-gray-600">
                      Valor padrão do sistema
                    </span>
                    <span className="text-lg font-bold text-green-600">
                      R$ 500,00
                    </span>
                  </div>
                  <p className="text-xs text-gray-500 mt-1">
                    Todas as carteiras iniciam com saldo de R$ 500,00
                  </p>
                </div>
              </div>
              <div className="space-y-2">
                <Label>Informações da Carteira</Label>
                <div className="p-3 bg-gray-50 rounded-lg border border-blue-200">
                  <div className="space-y-2 text-sm dark:text-black">
                    <div className="flex items-center space-x-2 ">
                      <CheckCircle className="h-4 w-4 text-blue-600" />
                      <span>Carteira vinculada ao usuário selecionado</span>
                    </div>
                    <div className="flex items-center space-x-2">
                      <CheckCircle className="h-4 w-4 text-blue-600" />
                      <span>Integração com sistema de notificações</span>
                    </div>
                  </div>
                </div>
              </div>
              <Button type="submit" className="w-full">
                <Wallet className="h-4 w-4 mr-2" />
                Criar Carteira
              </Button>
            </form>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Carteiras Cadastradas</CardTitle>
            <CardDescription>
              Lista de todas as carteiras do sistema
            </CardDescription>
          </CardHeader>
          <CardContent>
            <div className="space-y-4 max-h-96 overflow-y-auto">
              {users.map((user) => (
                <div
                  key={user.id}
                  className="flex items-center justify-between p-4 border border-border rounded-lg bg-gradient-to-r from-green-50 to-emerald-50 dark:from-green-950/20 dark:to-emerald-950/20"
                >
                  <div className="flex items-center space-x-4">
                    <div className="p-2 bg-green-100 dark:bg-green-900/30 rounded-full">
                      <Wallet className="h-5 w-5 text-green-600 dark:text-green-400" />
                    </div>
                    <div>
                      <p className="font-medium">{user.name}</p>
                      <p className="text-sm text-muted-foreground">
                        {user.email}
                      </p>
                      <div className="flex items-center space-x-2 mt-1">
                        <Badge
                          variant={
                            user.type === "User" ? "default" : "secondary"
                          }
                          className="text-xs"
                        >
                          {user.type === "User" ? "Usuário" : "Lojista"}
                        </Badge>
                      </div>
                    </div>
                  </div>
                  <div className="text-right space-y-2">
                    <div>
                      <p className="text-2xl font-bold text-green-600">
                        R$ {user.balance.toFixed(2)}
                      </p>
                      <p className="text-xs text-muted-foreground">
                        Saldo disponível
                      </p>
                    </div>
                    <div className="flex items-center space-x-1">
                      {user.type === "User" ? (
                        <>
                          <ArrowRightLeft className="h-3 w-3 text-green-500" />
                          <span className="text-xs text-green-600">
                            Pode enviar/receber
                          </span>
                        </>
                      ) : (
                        <>
                          <ArrowRightLeft className="h-3 w-3 text-blue-500 rotate-180" />
                          <span className="text-xs text-blue-600">
                            Apenas receber
                          </span>
                        </>
                      )}
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>
    </TabsContent>
  );
}
