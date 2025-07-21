import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "@/components/ui/select";
import { TabsContent } from "@/components/ui/tabs";
import { useState } from "react";

export function TabUser(){
    const [activeTab, setActiveTab] = useState("dashboard")
      const [userForm, setUserForm] = useState({
        fullName: "",
        email: "",
        cpf: "",
        password: "",
        userType: "",
      })
      const [transactionForm, setTransactionForm] = useState({
        amount: "",
        senderId: "",
        receiverId: "",
      })
    
      const users = [
        { id: "1", name: "João Silva", email: "joao@example.com", type: "User", cpf: "123.456.789-01", balance: 750.0 },
        {
          id: "2",
          name: "Maria Santos",
          email: "maria@example.com",
          type: "Merchant",
          cpf: "987.654.321-09",
          balance: 1250.0,
        },
        { id: "3", name: "Pedro Costa", email: "pedro@example.com", type: "User", cpf: "456.789.123-45", balance: 320.5 },
      ]
    
      const transactions = [
        {
          id: "1",
          from: "João Silva",
          to: "Maria Santos",
          amount: 150.0,
          status: "Completed",
          date: "2024-01-15 14:30",
          type: "transfer",
        },
        {
          id: "2",
          from: "Pedro Costa",
          to: "João Silva",
          amount: 75.5,
          status: "Completed",
          date: "2024-01-15 13:15",
          type: "transfer",
        },
        {
          id: "3",
          from: "João Silva",
          to: "Pedro Costa",
          amount: 200.0,
          status: "Failed",
          date: "2024-01-15 12:00",
          type: "transfer",
        },
        {
          id: "4",
          from: "Maria Santos",
          to: "João Silva",
          amount: 300.0,
          status: "Pending",
          date: "2024-01-15 11:45",
          type: "transfer",
        },
      ]
    
      const handleUserSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        console.log("User registration:", userForm)
        setUserForm({ fullName: "", email: "", cpf: "", password: "", userType: "" })
      }
    

    return (
        <TabsContent value="users" className="space-y-6">
            <div className="grid gap-6 md:grid-cols-2">
              <Card>
                <CardHeader>
                  <CardTitle>Cadastrar Novo Usuário</CardTitle>
                  <CardDescription>Criar usuário comum ou lojista</CardDescription>
                </CardHeader>
                <CardContent>
                  <form onSubmit={handleUserSubmit} className="space-y-4">
                    <div className="space-y-2">
                      <Label htmlFor="fullName">Nome Completo</Label>
                      <Input
                        id="fullName"
                        value={userForm.fullName}
                        onChange={(e) => setUserForm({ ...userForm, fullName: e.target.value })}
                        placeholder="João Silva"
                        required
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="email">Email</Label>
                      <Input
                        id="email"
                        type="email"
                        value={userForm.email}
                        onChange={(e) => setUserForm({ ...userForm, email: e.target.value })}
                        placeholder="joao@example.com"
                        required
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="cpf">CPF</Label>
                      <Input
                        id="cpf"
                        value={userForm.cpf}
                        onChange={(e) => setUserForm({ ...userForm, cpf: e.target.value })}
                        placeholder="123.456.789-01"
                        required
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="password">Senha</Label>
                      <Input
                        id="password"
                        type="password"
                        value={userForm.password}
                        onChange={(e) => setUserForm({ ...userForm, password: e.target.value })}
                        placeholder="••••••••"
                        required
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="userType">Tipo de Usuário</Label>
                      <Select
                        value={userForm.userType}
                        onValueChange={(value) => setUserForm({ ...userForm, userType: value })}
                      >
                        <SelectTrigger>
                          <SelectValue placeholder="Selecione o tipo" />
                        </SelectTrigger>
                        <SelectContent>
                          <SelectItem value="1">Usuário Comum</SelectItem>
                          <SelectItem value="2">Lojista</SelectItem>
                        </SelectContent>
                      </Select>
                    </div>
                    <Button type="submit" className="w-full">
                      Cadastrar Usuário
                    </Button>
                  </form>
                </CardContent>
              </Card>

              <Card>
                <CardHeader>
                  <CardTitle>Usuários Cadastrados</CardTitle>
                  <CardDescription>Lista de todos os usuários do sistema</CardDescription>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    {users.map((user) => (
                      <div key={user.id} className="flex items-center justify-between p-3 border rounded-lg">
                        <div className="flex items-center space-x-3">
                          <Avatar>
                            <AvatarFallback>
                              {user.name
                                .split(" ")
                                .map((n) => n[0])
                                .join("")}
                            </AvatarFallback>
                          </Avatar>
                          <div>
                            <p className="font-medium">{user.name}</p>
                            <p className="text-sm text-muted-foreground">{user.email}</p>
                            <p className="text-xs text-muted-foreground">{user.cpf}</p>
                          </div>
                        </div>
                        <div className="text-right">
                          <Badge variant={user.type === "User" ? "default" : "secondary"}>
                            {user.type === "User" ? "Usuário" : "Lojista"}
                          </Badge>
                          <p className="text-sm font-medium mt-1">R$ {user.balance.toFixed(2)}</p>
                        </div>
                      </div>
                    ))}
                  </div>
                </CardContent>
              </Card>
            </div>
          </TabsContent>
    )
}