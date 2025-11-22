import styles from './AdminMain.module.css';

export function AdminMain() {

    
	return (
		<div>
			<div className={styles['main-info']}>
				<p>
				Вы находитесь в сервисе "Цифровое портфолио". С помощью данного программного продукта студенты могут формировать свои портфолио
				</p>
				<p>
				Ваша задача проверять документы и просматривать необходимые портфолио! Удачи!
				</p>
			</div>
		</div>
	);
}