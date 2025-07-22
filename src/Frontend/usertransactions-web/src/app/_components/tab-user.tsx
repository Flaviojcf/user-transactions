import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
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
import { useState } from "react";
import { useApp } from "@/contexts/AppContext";

export function TabUser() {
  const { users, loading, error, success, createUser, clearMessages } =
    useApp();

  const [userForm, setUserForm] = useState({
    fullName: "",
    email: "",
    cpf: "",
    password: "",
    userType: "",
  });

  const handleUserSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createUser({
        fullName: userForm.fullName,
        email: userForm.email,
        cpf: userForm.cpf,
        password: userForm.password,
        userType: Number(userForm.userType),
      });

      setUserForm({
        fullName: "",
        email: "",
        cpf: "",
        password: "",
        userType: "",
      });
    } catch (err) {
      console.error("Erro ao cadastrar usuário:", err);
    }
  };

  return (
    <TabsContent value="users" className="space-y-6">
      <div className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Cadastrar Novo Usuário</CardTitle>
            <CardDescription>Criar usuário comum ou lojista</CardDescription>
          </CardHeader>
          <CardContent>
            {success && (
              <div className="mb-4 p-3 bg-green-100 border border-green-400 text-green-700 rounded">
                {success}
                <button
                  onClick={clearMessages}
                  className="ml-2 text-green-600 hover:text-green-800"
                >
                  ×
                </button>
              </div>
            )}
            <form onSubmit={handleUserSubmit} className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="fullName">Nome Completo</Label>
                <Input
                  id="fullName"
                  value={userForm.fullName}
                  onChange={(e) =>
                    setUserForm({ ...userForm, fullName: e.target.value })
                  }
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
                  onChange={(e) =>
                    setUserForm({ ...userForm, email: e.target.value })
                  }
                  placeholder="joao@example.com"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="cpf">CPF</Label>
                <Input
                  id="cpf"
                  value={userForm.cpf}
                  onChange={(e) =>
                    setUserForm({ ...userForm, cpf: e.target.value })
                  }
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
                  onChange={(e) =>
                    setUserForm({ ...userForm, password: e.target.value })
                  }
                  placeholder="••••••••"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="userType">Tipo de Usuário</Label>
                <Select
                  value={userForm.userType}
                  onValueChange={(value) =>
                    setUserForm({ ...userForm, userType: value })
                  }
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
              <Button type="submit" className="w-full" disabled={loading}>
                {loading ? "Cadastrando..." : "Cadastrar Usuário"}
              </Button>
            </form>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Usuários Cadastrados</CardTitle>
            <CardDescription>
              Lista de todos os usuários do sistema
            </CardDescription>
          </CardHeader>
          <CardContent>
            {loading ? (
              <p>Carregando...</p>
            ) : error ? (
              <p className="text-red-500">{error}</p>
            ) : (
              <div className="space-y-4 max-h-96 overflow-y-auto">
                {users.length === 0 ? (
                  <p>Nenhum usuário cadastrado.</p>
                ) : (
                  users.map((user) => (
                    <div
                      key={
                        `${user.id}` +
                        Math.random().toString(36).substring(2, 15)
                      }
                      className="flex items-center justify-between p-3 border rounded-lg"
                    >
                      <div className="flex items-center space-x-3">
                        <Avatar>
                          <AvatarFallback>
                            {user.fullName
                              .split(" ")
                              .map((n) => n[0])
                              .join("")}
                          </AvatarFallback>
                        </Avatar>
                        <div>
                          <p className="font-medium">{user.fullName}</p>
                          <p className="text-sm text-muted-foreground">
                            {user.email}
                          </p>
                          <p className="text-xs text-muted-foreground">
                            {user.cpf}
                          </p>
                        </div>
                      </div>
                      <div className="text-right">
                        <Badge
                          variant={
                            user.userType === 1 ? "default" : "secondary"
                          }
                        >
                          {user.userType === 1 ? "Usuário" : "Lojista"}
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
