import React from "react";

type MovieInfoProps = {
  biography: string;
  birthDate: string;
};


const ActorInfo : React.FC<MovieInfoProps> = ({biography, birthDate}) => {
  return (
    <div className="space-y-4">
      <h2 className="text-2xl font-bold">Об актере</h2>

      <div className="space-y-4">
        <span className="font-semibold">День рождения: </span>
        <span>{birthDate}</span>
      </div>
      <h2 className="text-2xl font-bold">Биография</h2>

      <div className="space-y-4 max-w-xl">
        {biography}
      </div>
    </div>
  );
};

export default ActorInfo;