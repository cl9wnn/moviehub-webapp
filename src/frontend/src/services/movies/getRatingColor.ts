export const getRatingColor = (rating: number | null): string => {
  if (rating === null) return "text-gray-500";
  if (rating < 3) return "text-red-600";
  if (rating < 5) return "text-orange-400";
  if (rating < 7) return "text-yellow-500";
  return "text-green-600";
};