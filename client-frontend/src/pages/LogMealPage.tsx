import { LogMealForm } from "@/components/LogMealForm";
import DashboardContentWrapper from "@/components/DashboardContentWrapper";

export default function UserProfilePage() {
  return (
    <DashboardContentWrapper>
      <LogMealForm mode={"create"} />
    </DashboardContentWrapper>
  );
}