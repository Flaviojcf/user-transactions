import { Tabs, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { useState } from "react";
import { TabDashboard } from "./tab-dashboard";
import { TabUser } from "./tab-user";
import TabWallet from "./tab-wallet";
import TabTransactions from "./tab-transactions";

export function Section() {
  const [activeTab, setActiveTab] = useState("dashboard");

  return (
    <div className="container mx-auto px-4 py-6 flex-grow">
      <Tabs
        value={activeTab}
        onValueChange={setActiveTab}
        className="space-y-6"
      >
        <TabsList className="grid w-full grid-cols-4">
          <TabsTrigger value="dashboard">Dashboard</TabsTrigger>
          <TabsTrigger value="users">Usuários</TabsTrigger>
          <TabsTrigger value="wallets">Carteiras</TabsTrigger>
          <TabsTrigger value="transactions">Transações</TabsTrigger>
        </TabsList>

        <TabDashboard />
        <TabUser />
        <TabWallet />
        <TabTransactions />
      </Tabs>
    </div>
  );
}
