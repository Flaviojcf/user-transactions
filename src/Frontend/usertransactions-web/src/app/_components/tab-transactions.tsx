import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@/components/ui/select";
import { TabsContent } from "@/components/ui/tabs";
import { CheckCircle } from "lucide-react";
import { useState } from "react";
import { useApp } from "@/contexts/AppContext";

export default function TabTransactions() {
  const { wallets, transactions, loading, createTransaction } = useApp();

  const [transactionForm, setTransactionForm] = useState({
    amount: "",
    senderId: "",
    receiverId: "",
  });

  const handleTransactionSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createTransaction({
        amount: parseFloat(transactionForm.amount),
        senderId: transactionForm.senderId,
        receiverId: transactionForm.receiverId,
      });

      setTransactionForm({ amount: "", senderId: "", receiverId: "" });
    } catch (err) {
      console.log("Erro ao criar transação:", err);
    }
  };

  const senderWallets = wallets.filter(w => w.userType === 1);

  return (
    <TabsContent value="transactions" className="space-y-6">
      <div className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Nova Transação</CardTitle>
            <CardDescription>Transferir valor entre carteiras</CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleTransactionSubmit} className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="amount">Valor (R$)</Label>
                <Input
                  id="amount"
                  type="number"
                  step="0.01"
                  value={transactionForm.amount}
                  onChange={(e) =>
                    setTransactionForm({
                      ...transactionForm,
                      amount: e.target.value,
                    })
                  }
                  placeholder="100.50"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="senderId">Remetente</Label>
                <Select
                  value={transactionForm.senderId}
                  onValueChange={(value) =>
                    setTransactionForm({ ...transactionForm, senderId: value })
                  }
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione o remetente" />
                  </SelectTrigger>
                  <SelectContent>
                    {senderWallets.map((wallet) => {
                      return (
                        <SelectItem key={wallet.id} value={wallet.id}>
                          {wallet.fullName} - R${" "}
                          {wallet?.balance.toFixed(2) || "0.00"}
                        </SelectItem>
                      );
                    })}
                  </SelectContent>
                </Select>
              </div>
              <div className="space-y-2">
                <Label htmlFor="receiverId">Destinatário</Label>
                <Select
                  value={transactionForm.receiverId}
                  onValueChange={(value) =>
                    setTransactionForm({
                      ...transactionForm,
                      receiverId: value,
                    })
                  }
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione o destinatário" />
                  </SelectTrigger>
                  <SelectContent>
                    {wallets.map((wallet) => (
                      <SelectItem key={wallet.id} value={wallet.id}>
                        {wallet.fullName} - R${" "}
                        {wallet?.balance.toFixed(2) || "0.00"}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <Button type="submit" className="w-full" disabled={loading}>
                {loading ? "Processando..." : "Processar Transação"}
              </Button>
            </form>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Histórico de Transações</CardTitle>
            <CardDescription>Todas as transações processadas</CardDescription>
          </CardHeader>
          <CardContent>
            {loading ? (
              <p>Carregando...</p>
            ) : (
              <div className="space-y-4 max-h-96 overflow-y-auto">
                {transactions.length === 0 ? (
                  <p>Nenhuma transação encontrada.</p>
                ) : (
                  transactions.map((transaction) => (
                    <div
                      key={transaction.id}
                      className="flex items-center justify-between p-3 border rounded-lg"
                    >
                      <div className="flex items-center space-x-3">
                        <div className="flex items-center">
                          <CheckCircle className="h-5 w-5 text-green-500" />
                        </div>
                        <div>
                          <p className="font-medium text-sm">
                            {transaction.senderName} →{" "}
                            {transaction.receiverName}
                          </p>
                          <p className="text-xs text-muted-foreground">
                            {new Date(transaction.createdAt).toLocaleString(
                              "pt-BR"
                            )}
                          </p>
                        </div>
                      </div>
                      <div className="text-right">
                        <p className="font-medium">
                          R$ {transaction.amount.toFixed(2)}
                        </p>
                        <Badge variant={"default"} className="text-xs">
                          Concluída
                        </Badge>
                      </div>
                    </div>
                  ))
                )}
              </div>
            )}
          </CardContent>
        </Card>
      </div>
    </TabsContent>
  );
}
