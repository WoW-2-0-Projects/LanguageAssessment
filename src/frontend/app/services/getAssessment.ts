import { Endpoints } from "@/constants";
import { AssessmentTest } from "@/types/Assessment";
import { useQuery } from "react-query";

export function getGrammarAssessment(testId: string) {
    return useQuery({
        queryFn: async () => {
            try {
                const response = await fetch(`${Endpoints.GetById}${testId}`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "ngrok-skip-browser-warning": "true"
                    }
                });
                const data = await response.json()
                console.log({data});
                return data as AssessmentTest
            } catch(error) {
                console.error(error);
            }
        },
        queryKey: ["assessment"]
    })
}