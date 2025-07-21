import { ModeToggle } from "./mode-toggle";

export function Header() {
  return (
    <div className="border-b">
      <div className="container mx-auto px-4 py-4">
        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <div className="flex items-center space-x-2">
              <h1 className="text-2xl font-bold text-gray-900 dark:text-white">
                Sitema de transações
              </h1>
            </div>
          </div>
          <ModeToggle/>
        </div>
      </div>
    </div>
  );
}
