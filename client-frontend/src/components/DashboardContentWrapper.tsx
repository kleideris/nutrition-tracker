type Props = {
  children: React.ReactNode;
};

export default function DashboardContentWrapper({ children }: Props) {
  return (
    <main className="min-h-[calc(100vh-80px)] w-full px-6 py-10">
      <div className="max-w-5xl mx-auto space-y-6">
        {children}
      </div>
    </main>
  );
}